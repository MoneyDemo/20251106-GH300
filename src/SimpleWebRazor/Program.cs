using System.Text;
using System.Text.Json;
using Ci.Extension.Core;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SimpleWebRazor.Models;
using TwentyTwenty.Storage;
using TwentyTwenty.Storage.Azure;
using TwentyTwenty.Storage.Local;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["APPINSIGHTS_CONNECTIONSTRING"]);
builder.Services.AddHealthChecks();

var storageType = builder.Configuration.GetValue<StorageType>("Storage:Type");
switch (storageType)
{
    case StorageType.Azure:
        var azureConnStr = builder.Configuration.GetValue<string>("Storage:Azure:ConnectionString");
        if (azureConnStr.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(azureConnStr));
        builder.Services.AddSingleton<IStorageProvider, AzureStorageProvider>(provider =>
            new AzureStorageProvider(new AzureProviderOptions() { ConnectionString = azureConnStr }));
        break;
    default:
        builder.Services.AddSingleton<IStorageProvider, LocalStorageProvider>(provider =>
            new LocalStorageProvider(Path.Combine(builder.Environment.WebRootPath, "")));
        break;
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapHealthChecks("/health", new HealthCheckOptions()
{
    ResponseWriter = WriteResponse
});

app.MapRazorPages();

app.Run();

/// <summary>
/// Health endpoint to json
/// </summary>
/// <param name="context"></param>
/// <param name="result"></param>
/// <remarks>https://docs.microsoft.com/zh-tw/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-5.0#customize-output</remarks>
static Task WriteResponse(HttpContext context, HealthReport result)
{
    context.Response.ContentType = "application/json; charset=utf-8";

    var options = new JsonWriterOptions
    {
        Indented = true
    };

    using var stream = new MemoryStream();
    using (var writer = new Utf8JsonWriter(stream, options))
    {
        writer.WriteStartObject();
        writer.WriteString("status", result.Status.ToString());
        writer.WriteStartObject("results");
        foreach (var entry in result.Entries)
        {
            writer.WriteStartObject(entry.Key);
            writer.WriteString("status", entry.Value.Status.ToString());
            writer.WriteString("description", entry.Value.Description);
            writer.WriteStartObject("data");
            foreach (var item in entry.Value.Data)
            {
                writer.WritePropertyName(item.Key);
                JsonSerializer.Serialize(
                    writer, item.Value, item.Value?.GetType() ??
                                        typeof(object));
            }
            writer.WriteEndObject();
            writer.WriteEndObject();
        }
        writer.WriteEndObject();
        writer.WriteEndObject();
    }

    var json = Encoding.UTF8.GetString(stream.ToArray());

    return context.Response.WriteAsync(json);
}

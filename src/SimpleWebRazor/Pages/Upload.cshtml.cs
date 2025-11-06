using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SimpleWebRazor.Models;
using TwentyTwenty.Storage;

namespace SimpleWebRazor.Pages;

public class UploadModel : PageModel
{
    private readonly ILogger<UploadModel> _logger;
    private readonly IWebHostEnvironment _env;
    private readonly IStorageProvider _storageProvider;
    private readonly IConfiguration _config;

    public string CloudUrl { get; set; } = string.Empty;
    public string LocalUrl { get; set; } = string.Empty;

    public UploadModel(ILogger<UploadModel> logger, IWebHostEnvironment env, IStorageProvider storageProvider, IConfiguration config)
    {
        _logger = logger;
        _env = env;
        _storageProvider = storageProvider;
        _config = config;
    }

    public void OnGet()
    {
        var fileName = _config.GetValue<string>("Storage:FileName");
        CloudUrl = _storageProvider.GetBlobSasUrl("simpleweb", fileName,
            DateTimeOffset.Now.AddDays(1));
        LocalUrl = $"~/uploads/{fileName}";
    }

    public async Task<IActionResult> OnPostAsync(IFormFile? imgFile)
    {
        if (imgFile != null && imgFile.Length > 0)
        {
            var storageType = _config.GetValue<StorageType>("Storage:Type");
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imgFile.FileName)}";
            var containerName = "simpleweb";
            if (storageType == StorageType.Local)
                containerName = "uploads";
            await _storageProvider.SaveBlobStreamAsync(containerName, $"{fileName}", imgFile.OpenReadStream())
                .ConfigureAwait(false);

            // update appsettings.json
            var jsonText = System.IO.File.ReadAllText(Path.Combine(_env.ContentRootPath, "appsettings.json"));
            var jsonObj = JsonConvert.DeserializeObject<dynamic>(jsonText);
            if (jsonObj != null)
            {
                jsonObj["Storage"]["FileName"] = fileName;

                System.IO.File.WriteAllText(Path.Combine(_env.ContentRootPath, "appsettings.json"),
                    JsonConvert.SerializeObject(jsonObj, Formatting.Indented));
            }
        }

        return RedirectToPage("./Upload");
    }
}

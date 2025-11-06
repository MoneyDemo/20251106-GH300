using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SimpleWebRazor.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public string UserName { get; set; } = string.Empty;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        string identifier = "X-MS-CLIENT-PRINCIPAL-NAME";
        IEnumerable<string> headerValues = HttpContext.Request.Headers[identifier];
        if (!headerValues.Any())
            UserName = "Not login yet";
        else
            UserName = headerValues.FirstOrDefault() ?? "Not login yet";
    }
}

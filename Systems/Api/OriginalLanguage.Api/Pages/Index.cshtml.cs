namespace OriginalLanguage.Api.Pages;

using OriginalLanguage.Common;
using OriginalLanguage.Services.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;
using OriginalLanguage.Api.Settings;

public class IndexModel : PageModel
{
    [BindProperty]
    public bool OpenApiEnabled => openApiSettings.Enabled;

    [BindProperty]
    public string? Version => Assembly.GetExecutingAssembly().GetAssemblyVersion();

    [BindProperty]
    public string? HelloMessage => apiSettings.HelloMessage;


    private readonly OpenApiSettings openApiSettings;
    private readonly ApiSpecialSettings apiSettings;

    public IndexModel(OpenApiSettings settings, ApiSpecialSettings apiSettings)
    {
        this.openApiSettings = settings;
        this.apiSettings = apiSettings;
    }

    public void OnGet()
    {
    }
}

using Duende.IdentityServer.Models;

namespace OriginalLanguage.Identity.Configuration;

public class AppIdentityResources
{
    public static IEnumerable<IdentityResource> Resources => new List<IdentityResource>
    {
        new IdentityResources.Profile(),
        new IdentityResources.OpenId()
    };
}

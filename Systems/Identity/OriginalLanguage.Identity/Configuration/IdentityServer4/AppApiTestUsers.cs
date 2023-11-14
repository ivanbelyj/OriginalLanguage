using Duende.IdentityServer.Test;

namespace OriginalLanguage.Identity.Configuration;

public static class AppApiTestUsers
{
    public static List<TestUser> ApiTestUsers =>
        new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "1",
                Username = "off0zga@test.com",
                Password = "password"
            },
            new TestUser
            {
                SubjectId = "2",
                Username = "test@test.com",
                Password = "password"
            }
        };
}

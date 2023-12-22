using OriginalLanguage.Common.Utils;

namespace OriginalLanguage.Api.Configuration;

public static class AutoMapperConfiguration
{
    public static IServiceCollection AddAppAutoMapper(this IServiceCollection services)
    {
        AutoMapperUtils.AddAppAutoMapper(services, "originallanguage.");

        return services;
    }
}

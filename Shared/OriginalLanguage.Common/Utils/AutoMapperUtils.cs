namespace OriginalLanguage.Common.Utils;

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

public static class AutoMapperUtils
{
    public static void AddAppAutoMapper(IServiceCollection services, string appPrefix)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(s => s.FullName != null
                && s.FullName.ToLower().StartsWith(appPrefix));

        services.AddAutoMapper(assemblies);
    }
}

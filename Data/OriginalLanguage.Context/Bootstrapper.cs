using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OriginalLanguage.Context.Factories;
using OriginalLanguage.Context.Settings;
using OriginalLanguage.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Context;
public static class Bootstrapper
{
    public static IServiceCollection AddAppDbContext(this IServiceCollection services,
        IConfiguration? configuration = null)
    {
        DbSettings? dbSettings = ConfigurationUtils.Load<DbSettings>("Database",
            configuration);
        if (dbSettings == null)
            throw new Exception("Database configuration cannot be null");

        services.AddSingleton(dbSettings);

        var dbInitOptionsDelegate = DbContextOptionsFactory.GetConfigureDelegate(
            dbSettings.ConnectionString, dbSettings.Type);

        services.AddDbContextFactory<MainDbContext>(dbInitOptionsDelegate);

        return services;
    }
}

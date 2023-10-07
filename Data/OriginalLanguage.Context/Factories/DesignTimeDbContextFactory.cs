using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using OriginalLanguage.Context.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Context.Factories;
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MainDbContext>
{
    public MainDbContext CreateDbContext(string[] args)
    {
        string provider = (args?[0] ?? $"{DbType.MSSQL}").ToLower();
        var configuration = new ConfigurationBuilder()
             .AddJsonFile("appsettings.context.json")
             .Build();

        string? connectionString = configuration.GetConnectionString(provider);

        DbContextOptions<MainDbContext> options;
        if (provider.Equals($"{DbType.MSSQL}".ToLower()))
        {
            options = new DbContextOptionsBuilder<MainDbContext>()
                .UseSqlServer(
                    connectionString,
                    opts => opts.MigrationsAssembly(
                        $"{DbContextOptionsFactory.migrationProjctPrefix}{DbType.MSSQL}")
                )
                .Options;
        }
        else if (provider.Equals($"{DbType.PostgreSQL}".ToLower()))
        {
            options = new DbContextOptionsBuilder<MainDbContext>()
                .UseNpgsql(
                    connectionString,
                    opts => opts.MigrationsAssembly(
                        $"{DbContextOptionsFactory.migrationProjctPrefix}{DbType.PostgreSQL}")
                )
                .Options;
        }
        else
        {
            throw new Exception($"Unsupported provider: {provider}");
        }

        var dbContextFactory = new DbContextFactory(options);
        return dbContextFactory.Create();
    }
}

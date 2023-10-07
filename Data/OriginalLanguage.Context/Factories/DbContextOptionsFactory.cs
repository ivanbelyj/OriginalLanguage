using Microsoft.EntityFrameworkCore;
using OriginalLanguage.Context.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Context.Factories;
public class DbContextOptionsFactory
{
    public const string migrationProjctPrefix = $"OriginalLanguage.Context.Migrations";
    public static Action<DbContextOptionsBuilder> GetConfigureDelegate(
        string connectionString, DbType dbType)
    {
        return (builder) =>
        {
            switch (dbType)
            {
                case DbType.MSSQL:
                    builder.UseSqlServer(connectionString, opts => opts
                        .CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)
                        .MigrationsHistoryTable("_EFMigrationsHistory", "public")
                        .MigrationsAssembly($"{migrationProjctPrefix}{DbType.MSSQL}")
                    );
                    break;
                case DbType.PostgreSQL:
                    builder.UseNpgsql(connectionString, opts => opts
                        .CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)
                        .MigrationsHistoryTable("_EFMigrationsHistory", "public")
                        .MigrationsAssembly($"{migrationProjctPrefix}{DbType.PostgreSQL}")
                    );
                    break;
            }

            builder.EnableSensitiveDataLogging();
            //builder.UseLazyLoadingProxies();
            builder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        };
    }
}

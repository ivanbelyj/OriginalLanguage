using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Context.Setup;
public static class DbInitializer
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>()
            .CreateScope();
        ArgumentNullException.ThrowIfNull(scope);

        var dbContextFactory = scope.ServiceProvider
            .GetRequiredService<IDbContextFactory<MainDbContext>>();
        using var dbContext = dbContextFactory.CreateDbContext();

        dbContext.Database.Migrate();
    }
}

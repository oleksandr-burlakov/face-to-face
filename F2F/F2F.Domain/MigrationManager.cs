using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace F2F.DLL;

public static class MigrationManager
{
    public static async Task MigrateAsync(IServiceProvider services)
    {
        var context = services.GetRequiredService<F2FContext>();

        if (context.Database.IsSqlServer())
            await context.Database.MigrateAsync();
    }
}

using F2F.DLL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace F2F.DLL;

public static class DataAccessDependencyInjection
{
    public static IServiceCollection AddDataAccess(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDatabase(configuration);

        services.AddIdentity();

        services.AddRepositories();

        return services;
    }

    private static void AddRepositories(this IServiceCollection services) { }

    private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetSection("Database")["ConnectionString"];
        if (connectionString is null)
            return;
        services.AddDbContext<F2FContext>(
            options =>
                options.UseSqlServer(
                    connectionString,
                    opt => opt.MigrationsAssembly(typeof(F2FContext).Assembly.FullName)
                )
        );
    }

    private static void AddIdentity(this IServiceCollection services)
    {
        services
            .AddIdentityCore<User>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddRoles<Role>()
            .AddEntityFrameworkStores<F2FContext>()
            .AddSignInManager()
            .AddTokenProvider(
                TokenOptions.DefaultProvider,
                typeof(DataProtectorTokenProvider<User>)
            )
            .AddTokenProvider(TokenOptions.DefaultEmailProvider, typeof(EmailTokenProvider<User>))
            .AddTokenProvider(
                TokenOptions.DefaultPhoneProvider,
                typeof(PhoneNumberTokenProvider<User>)
            )
            .AddTokenProvider(
                TokenOptions.DefaultAuthenticatorProvider,
                typeof(AuthenticatorTokenProvider<User>)
            );

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 0;

            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = true;
        });
    }
}

public class ConfigurationOptions
{
    public string ConnectionString { get; set; }
}

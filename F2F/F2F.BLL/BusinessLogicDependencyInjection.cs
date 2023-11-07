using F2F.BLL.MappingProfiles;
using F2F.BLL.Services;
using F2F.BLL.Services.Realization;
using Microsoft.Extensions.DependencyInjection;

namespace F2F.BLL;

public static class BusinessLogicDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddServices();

        services.RegisterAutoMapper();

        return services;
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IClaimService, ClaimService>();
        services.AddScoped<IUserService, UserService>();
    }

    private static void RegisterAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(IMappingProfilesMarker));
    }
}

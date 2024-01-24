using F2F.BLL.MappingProfiles;
using F2F.BLL.Services;
using F2F.BLL.Services.Realization;
using Microsoft.Extensions.DependencyInjection;

namespace F2F.BLL;

public static class BusinessLogicDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services.AddServices().RegisterAutoMapper();
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IClaimService, ClaimService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IQuestionnaireService, QuestionnaireService>();
        services.AddScoped<IQuestionService, QuestionService>();
        services.AddScoped<IMeetingService, MeetingService>();
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<IMeetingParticipantService, MeetingParticipantService>();
        return services;
    }

    private static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
    {
        return services.AddAutoMapper(typeof(IMappingProfilesMarker));
    }
}

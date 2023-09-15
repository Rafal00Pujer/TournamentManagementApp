using Microsoft.Extensions.DependencyInjection;
using TournamentManagementLogic.Database;
using TournamentManagementLogic.Service.Interfaces;
using TournamentManagementLogic.Service;

namespace TournamentManagementLogic.Config;

public static class ConfigExtensionsMethods
{
    public static IServiceCollection AddLogicServices(this IServiceCollection services)
    {
        services.AddScoped<IDatabase, JsonDatabase>();
        services.AddScoped<ITournamentService, TournamentService>();
        services.AddScoped<ITournamentRoundsService, TournamentRoundsService>();
        services.AddScoped<ITeamService, TeamService>();
        services.AddScoped<IMatchService, MatchService>();
        services.AddScoped<ISetService, SetService>();

        return services;
    }
}


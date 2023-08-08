using Terminal.Gui;
using TournamentManagementConsoleUi.Logic.Database;
using TournamentManagementConsoleUi.Logic.Service;
using TournamentManagementConsoleUi.Logic.Service.Interfaces;
using TournamentManagementConsoleUi.View.Windows;

namespace TournamentManagementConsoleUi;

internal static class Program
{
    private static Toplevel? _nextTopLevel = null;

    private static IDatabase Database { get; } = new JsonDatabase();

    public static ITournamentService TournamentService { get; } = new TournamentService(Database);
    public static ITeamService TeamService { get; } = new TeamService(Database);
    public static IMatchService MatchService { get; } = new MatchService(Database);
    public static ISetService SetService { get; } = new SetService(Database);

    private static void Main(string[] args)
    {
        Application.Init();

        _nextTopLevel = new TorunamentsListWindow();

        try
        {
            while (true)
            {
                var tempNextTopLevel = _nextTopLevel;
                _nextTopLevel = null;

                Application.Run(tempNextTopLevel);

                if (_nextTopLevel is null)
                {
                    break;
                }
            }
        }
        finally
        {
            Application.Shutdown();
        }
    }

    public static void ChangeTopLevel(Toplevel newTopLevel)
    {
        _nextTopLevel = newTopLevel;

        Application.RequestStop();
    }
}
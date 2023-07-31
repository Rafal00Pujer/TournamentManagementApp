using Terminal.Gui;
using TournamentManagementConsoleUi;

Application.Init();

try
{
    Application.Run(new MyView());
}
finally
{
    Application.Shutdown();
}
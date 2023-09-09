using TournamentManagementLogic.Database;
using TournamentManagementLogic.Service.Interfaces;
using TournamentManagementLogic.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var mvcBuilder = builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IDatabase, JsonDatabase>();
builder.Services.AddSingleton<ITournamentService, TournamentService>();
builder.Services.AddSingleton<ITeamService, TeamService>();
builder.Services.AddSingleton<IMatchService, MatchService>();
builder.Services.AddSingleton<ISetService, SetService>();

if (builder.Environment.IsDevelopment())
{
    mvcBuilder.AddRazorRuntimeCompilation();
}


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{tournamentId?}");

app.Run();

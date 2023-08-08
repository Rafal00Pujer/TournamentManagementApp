namespace TournamentManagementConsoleUi.Logic.Database;

public record DatabaseRecord(Guid Id);

public record TournamentRecord(Guid Id, string Name, string GameName, string Description) : DatabaseRecord(Id);

public record TeamRecord(Guid Id, string Name, Guid TournamentId) : DatabaseRecord(Id);

public record MatchRecord(Guid Id, Guid TournamentId, DateOnly? Date, Guid? FirstPreviousMach, Guid? SecondPreviousMach, Guid? FirstTeam, Guid? SecondTeam, Guid? Winner) : DatabaseRecord(Id);

public record SetRecord(Guid Id, Guid MatchId, int SetNumber, int FirstTeamScore, int SecondTeamScore) : DatabaseRecord(Id);

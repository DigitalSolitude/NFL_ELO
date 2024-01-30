using NFL_ELO;
using System.Data;

internal class Program
{
    private static double playoffKFactor = 64;
    private static double regularSeasonKFactor = 32;

    private static void Main(string[] args)
    {
        CSV_Manager manager = new CSV_Manager();
        TeamList teamList = new TeamList();
        var seasons = manager.GetDistinctValues(manager.Table, "season");
        foreach (var season in seasons)
        {
            Console.WriteLine($"Printing Data for {season} Season");
            DataRow[] seasonRows = manager.Table.Select($"season = '{season}'");
            var distinctGameweeks = manager.GetDistinctValues(seasonRows.CopyToDataTable(), "week");
            foreach (var week in distinctGameweeks)
            {
                Console.WriteLine($"Printing Data for week {week}");
                DataRow[] gameweekRows = seasonRows.Where(row => row["week"].ToString() == week).ToArray();
                foreach (var row in gameweekRows)
                {
                    string homeTeam = row["home_team"].ToString();
                    int homeScore = Convert.ToInt32(row["home_score"]);
                    string awayTeam = row["away_team"].ToString();
                    int awayScore = Convert.ToInt32(row["away_score"]);

                    Team home = teamList.Teams.FirstOrDefault(team => team.ShortName.Contains(homeTeam));
                    Team away = teamList.Teams.FirstOrDefault(team => team.ShortName.Contains(awayTeam));

                    int homeELOOld = home.ELO;
                    int awayELOOld = away.ELO;

                    CalculateELO(home, homeScore, away, awayScore, int.TryParse(row.ToString(), out _));
                    Console.WriteLine($"Team : {home.LongName} ELO : {home.ELO} Change : {homeELOOld - home.ELO}");
                    Console.WriteLine($"Team : {away.LongName} ELO : {away.ELO} Change : {awayELOOld - away.ELO}");

                }
            }
        }

        void CalculateELO(Team homeTeam, int homeScore, Team awayTeam, int awayScore, bool homeFieldAdvantage)
        {
            double expectedHome = CalculateExpectedOutcome(homeTeam.ELO, awayTeam.ELO);
            double expectedAway = 1 - expectedHome;

            double outcomeHome = homeScore > awayScore ? 1.0 : homeScore == awayScore ? 0.5 : 0.0;
            double outcomeAway = 1 - outcomeHome;

            double kFactor = homeFieldAdvantage ? playoffKFactor : regularSeasonKFactor;

            homeTeam.ELO = (int)(homeTeam.ELO + kFactor * (outcomeHome - expectedHome));
            awayTeam.ELO = (int)(awayTeam.ELO + kFactor * (outcomeAway - expectedAway));
        }
    }

    private static double CalculateExpectedOutcome(int eLO1, int eLO2)
    {
        double exponent = (eLO2 - eLO1) / 400.0;
        double expectedOutcomeA = 1.0 / (1.0 + Math.Pow(10, exponent));
        return expectedOutcomeA;
    }
}
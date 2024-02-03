using NFL_ELO;
using System.Data;
using Spectre.Console;

internal class Program
{
    private static double playoffKFactor = 128;
    private static double regularSeasonKFactor = 32;

    private static void Main(string[] args)
    {
        CSV_Manager manager = new CSV_Manager();
        TeamList teamList = new TeamList();
        var seasons = manager.GetDistinctValues(manager.Table, "season");
        foreach (var season in seasons)
        {
            AnsiConsole.WriteLine($"Printing Data for {season} Season");
            DataRow[] seasonRows = manager.Table.Select($"season = '{season}'");
            var distinctGameweeks = manager.GetDistinctValues(seasonRows.CopyToDataTable(), "week");
            foreach (var week in distinctGameweeks)
            {
                var table = new DataTable();
                table.Columns.Add("Team");
                table.Columns.Add("ELO");
                table.Columns.Add("ELO Change");

                //table.AddColumns("Team", "ELO", "ELO Change");
                AnsiConsole.WriteLine($"Printing Data for week {week}");
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
                    table.Rows.Add(new string[] { home.LongName, home.ELO.ToString(), (home.ELO - homeELOOld).ToString() });
                    table.Rows.Add(new string[] { away.LongName, away.ELO.ToString(), (away.ELO - awayELOOld).ToString() });
                    //table.AddRow(new string[] {home.LongName, home.ELO.ToString(), (home.ELO-homeELOOld).ToString() });
                    //table.AddRow(new string[] { away.LongName, away.ELO.ToString(), (away.ELO - awayELOOld).ToString() });
                }
                

                DataTable sortedTable = new DataTable();
                sortedTable.Columns.Add("Team");
                sortedTable.Columns.Add("ELO");
                sortedTable.Columns.Add("ELO Change");

                while (table.Rows.Count > 0)
                {
                    int highestELO = 0;
                    int highestELOIndex = -1;
                    foreach (DataRow dataRow in table.Rows)
                    {

                        Int32.TryParse((string?)dataRow.ItemArray[1], out int currELO);
                        if (currELO > highestELO)
                        {
                            highestELO = currELO;
                            highestELOIndex = table.Rows.IndexOf(dataRow);
                        }
                    }
                    sortedTable.ImportRow(table.Rows[highestELOIndex]);
                    table.Rows.RemoveAt(highestELOIndex);
                }

                foreach (DataRow dataRow in sortedTable.Rows)
                {
                    Console.WriteLine($"{dataRow[0]} {dataRow[1]} {dataRow[2]}");
                    //AnsiConsole.Write(table);
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
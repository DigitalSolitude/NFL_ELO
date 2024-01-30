using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFL_ELO
{
    internal class TeamList
    {
        public List<Team> Teams { get; }

        public TeamList()
        {
            Teams = new List<Team>();
            Teams.Add(new Team("Arizona Cardinals", new string[] { "ARI" }));
            Teams.Add(new Team("Atlanta Falcons", new string[] { "ATL" }));
            Teams.Add(new Team("Baltimore Ravens", new string[] { "BAL" }));
            Teams.Add(new Team("Buffalo Bills", new string[] { "BUF"}));
            Teams.Add(new Team("Carolina Panthers", new string[] { "CAR"}));
            Teams.Add(new Team("Chicago Bears", new string[] { "CHI"}));
            Teams.Add(new Team("Cinncinati Bengals", new string[] { "CIN"}));
            Teams.Add(new Team("Cleveland Browns", new string[] { "CLE"}));
            Teams.Add(new Team("Dallas Cowboys", new string[] { "DAL"}));
            Teams.Add(new Team("Denver Bronchos", new string[] { "DEN"}));
            Teams.Add(new Team("Detroit Lions", new string[] { "DET"}));
            Teams.Add(new Team("Green Bay", new string[] { "GB"}));
            Teams.Add(new Team("Houstan Texans", new string[] { "HOU"}));
            Teams.Add(new Team("Indianapolis Colts", new string[] { "IND"}));
            Teams.Add(new Team("Jacksonville Jaguars", new string[] { "JAX"}));
            Teams.Add(new Team("Kansas City Chiefs", new string[] { "KC"}));
            Teams.Add(new Team("Miami Dolphins", new string[] { "MIA"}));
            Teams.Add(new Team("Minnesota Vikings", new string[] { "MIN"}));
            Teams.Add(new Team("New England Patriots", new string[] { "NE"}));
            Teams.Add(new Team("New Orleans Saints", new string[] { "NO"}));
            Teams.Add(new Team("New York Giants", new string[] { "NYG"}));
            Teams.Add(new Team("New York Jets", new string[] { "NYJ"}));
            Teams.Add(new Team("Los Vegas Raiders", new string[] { "OAK", "LV" }));
            Teams.Add(new Team("Philidelphia Eagles", new string[] { "PHI"}));
            Teams.Add(new Team("Pittsburg Steelers", new string[] { "PIT"}));
            Teams.Add(new Team("Los Angeles Chargers", new string[] { "LAC", "SD" }));
            Teams.Add(new Team("Seattle Seahawks", new string[] { "SEA"}));
            Teams.Add(new Team("San Francisco 49ers", new string[] { "SF"}));
            Teams.Add(new Team("Los Angeles Rams", new string[] {"LA", "STL"}));
            Teams.Add(new Team("Tampa Bay Buccaneers", new string[] { "TB"}));
            Teams.Add(new Team("Tennesee Titans", new string[] { "TEN"}));
            Teams.Add(new Team("Washington Commanders", new string[] { "WAS"}));
        }
    }
}
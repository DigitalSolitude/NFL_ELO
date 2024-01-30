using System.Collections;

namespace NFL_ELO
{
    public class Team
    {
        public string LongName;
        public string[] ShortName;
        public int ELO = 1000;
        public int ELO_Change;

        public Team(string longName, string[] shortName) 
        { 
            LongName = longName;
            ShortName = shortName;
        }
    }
}
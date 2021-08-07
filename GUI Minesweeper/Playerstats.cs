using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_Minesweeper
{
    public class Playerstats: IComparable <Playerstats>
    {
        public string playerName { get; set; } = "";
        public string gameDifficulty { get; set; } = "";
        public string gameTime { get; set; } = "";
        public int playerScore { get; set; } = 0;

        public int CompareTo(Playerstats other)
        {
            if (this.playerScore == other.playerScore) 
            {
                return this.playerScore.CompareTo(other.playerScore);
            }
            else 
            {
                return other.playerScore.CompareTo(this.playerScore);
            }
        }

    }
}

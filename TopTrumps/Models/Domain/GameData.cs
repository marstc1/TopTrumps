using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TopTrumps.Models.Domain
{
    public class GameData
    {
        public GameData()
        {
            this.CardsInPlay = new List<Card>();
            this.GameOver = false;
        }
        
        public List<Player> Players { get; set; }

        public List<Card> CardsInPlay { get; set; }

        public bool GameOver { get; set; }
    }
}
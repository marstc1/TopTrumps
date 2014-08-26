using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TopTrumps.Models.Domain
{
    public class Game
    {
        public Game()
        {
            this.CardsInPlay = new List<Card>();
        }
        
        public List<Player> Players { get; set; }

        public List<Card> CardsInPlay { get; set; }
    }
}
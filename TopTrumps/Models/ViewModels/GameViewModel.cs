namespace TopTrumps.Models.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;

    using TopTrumps.Models.Domain;

    public class GameViewModel
    {
        public string PlayersName { get; set; }

        public int PlayersNumberOfCards { get; set; }

        public Card PlayersCurrentCard { get; set; }

        public string SelectedOption { get; set; }

        public int ComputersNumberOfCards { get; set; }

        public GameViewModel(List<Player> players)
        {
            this.PlayersName = players[0].Name;
            this.PlayersNumberOfCards = players[0].Hand.Count;
            this.PlayersCurrentCard = players[0].Hand.FirstOrDefault();

            this.ComputersNumberOfCards = players[1].Hand.Count;
        }

        public GameViewModel(GameDetails gameDetails)
        {
            this.PlayersName = gameDetails.Players[0].Name;
            this.PlayersNumberOfCards = gameDetails.Players[0].Hand.Count;
            this.PlayersCurrentCard = gameDetails.Players[0].Hand.FirstOrDefault();

            this.ComputersNumberOfCards = gameDetails.Players[1].Hand.Count;
        }
    }
}
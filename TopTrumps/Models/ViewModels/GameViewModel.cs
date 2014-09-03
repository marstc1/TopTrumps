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

        public Card ComputersCurrentCard { get; set; }

        public bool PlayerInControl { get; set; }

        public GameViewModel(IReadOnlyList<Player> players)
        {
            PlayersName = players[1].Name;
            PlayersNumberOfCards = players[1].Hand.Count;
            PlayersCurrentCard = players[1].Hand.FirstOrDefault();
            PlayerInControl = players[1].InControlOfGame;

            ComputersNumberOfCards = players[0].Hand.Count;
            ComputersCurrentCard = players[0].Hand.FirstOrDefault();
        }
    }
}
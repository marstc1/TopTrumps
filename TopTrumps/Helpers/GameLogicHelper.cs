// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameLogicHelper.cs" company="KSS">
//   1997 - 2014
// </copyright>
// <summary>
//   Defines the GameLogicHelper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TopTrumps.Helpers
{
    using System.Linq;

    using TopTrumps.Models.Domain;

    /// <summary>
    /// The game logic class.
    /// </summary>
    public class GameLogicHelper
    {
        /// <summary>
        /// The game
        /// </summary>
        private readonly Game game;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameLogicHelper"/> class.
        /// </summary>
        /// <param name="game">The game.</param>
        public GameLogicHelper(Game game)
        {
            this.game = game;
        }
        
        /// <summary>
        /// Compares the cards.
        /// </summary>
        public void CompareCards()
        {
            var playersInGame = this.game.Players.Where(player => !player.IsOut).ToList();

            var cardsToCompare = playersInGame.Select(player => player.Hand.First()).ToList();

            var winningValue = cardsToCompare.Max(x => x.Strength);

            var isWinningCard = cardsToCompare.Count(x => x.Strength == winningValue) == 1;

            if (isWinningCard)
            {
                var winningCard = cardsToCompare.First(x => x.Strength == winningValue);
                var winningPlayer = this.game.Players.First(x => x.Hand.Contains(winningCard));

                this.SetPlayerInControl(winningPlayer);

                this.CollectCards(winningPlayer);
            }
            else
            {
                foreach (var player in playersInGame)
                {
                    var playersCard = player.Hand.First();
                    player.Hand.Remove(playersCard);
                    this.game.CardsInPlay.Add(playersCard);
                }
            }
        }

        /// <summary>
        /// Deals the cards.
        /// </summary>
        private void DealCards()
        {
            foreach (var player in this.game.Players)
            {
                player.Hand.Clear();
            }

            while (this.game.Deck.Cards.Any())
            {
                foreach (var player in this.game.Players)
                {
                    if (this.game.Deck.Cards.Any())
                    {
                        player.Hand.Add(this.game.Deck.TakeCard());
                    }
                }
            }
        }

        /// <summary>
        /// Sets the player in control.
        /// </summary>
        /// <param name="winningPlayer">The winning player.</param>
        private void SetPlayerInControl(Player winningPlayer)
        {
            foreach (var player in this.game.Players)
            {
                player.InControlOfGame = false;
            }

            winningPlayer.InControlOfGame = true;
        }

        /// <summary>
        /// Collects the cards.
        /// </summary>
        /// <param name="winningPlayer">The winning player.</param>
        private void CollectCards(Player winningPlayer)
        {
            var playersInGame = this.game.Players.Where(player => !player.IsOut).ToList();

            foreach (var player in playersInGame)
            {
                var playersCard = player.Hand.First();
                player.Hand.Remove(playersCard);
                winningPlayer.Hand.Add(playersCard);
            }

            if (this.game.CardsInPlay.Count > 0)
            {
                winningPlayer.Hand.AddRange(this.game.CardsInPlay);
                this.game.CardsInPlay.Clear();
            }
        }
    }
}
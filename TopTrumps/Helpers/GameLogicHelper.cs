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

    using Models.Domain;

    /// <summary>
    /// The game logic class.
    /// </summary>
    public class GameLogicHelper
    {
        /// <summary>
        /// The game
        /// </summary>
        private readonly Game _game;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameLogicHelper"/> class.
        /// </summary>
        /// <param name="game">The game.</param>
        public GameLogicHelper(Game game)
        {
            _game = game;
        }

        /// <summary>
        /// Compares the cards.
        /// </summary>
        /// <param name="propertyToCompare">The property to compare.</param>
        public void CompareCards(string propertyToCompare)
        {
            var playersInGame = _game.Players.Where(player => !player.IsOut).ToList();

            var cardsToCompare = playersInGame.Select(player => player.Hand.First()).ToList();

            var winningValue = cardsToCompare.Max(x => x.GetType().GetProperty(propertyToCompare).GetValue(x)).ToString();

            var numberOfWinningCards = 0;

            foreach (var card in cardsToCompare)
            {
                var cardValue = card.GetType().GetProperty(propertyToCompare).GetValue(card).ToString();

                if (cardValue == winningValue)
                {
                    numberOfWinningCards ++;
                }
            }

            if (numberOfWinningCards == 1)
            {
                var winningCard = cardsToCompare.First(x => x.GetType().GetProperty(propertyToCompare).GetValue(x).ToString() == winningValue);
                var winningPlayer = _game.Players.First(x => x.Hand.Contains(winningCard));

                SetPlayerInControl(winningPlayer);

                CollectCards(winningPlayer);
            }
            else
            {
                foreach (var player in playersInGame)
                {
                    var playersCard = player.Hand.First();
                    player.Hand.Remove(playersCard);
                    _game.CardsInPlay.Add(playersCard);
                }
            }
        }

        /// <summary>
        /// Sets the player in control.
        /// </summary>
        /// <param name="winningPlayer">The winning player.</param>
        private void SetPlayerInControl(Player winningPlayer)
        {
            foreach (var player in _game.Players)
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
            var playersInGame = _game.Players.Where(player => !player.IsOut).ToList();

            foreach (var player in playersInGame)
            {
                var playersCard = player.Hand.First();
                player.Hand.Remove(playersCard);
                winningPlayer.Hand.Add(playersCard);
            }

            if (_game.CardsInPlay.Count > 0)
            {
                winningPlayer.Hand.AddRange(_game.CardsInPlay);
                _game.CardsInPlay.Clear();
            }
        }
    }
}
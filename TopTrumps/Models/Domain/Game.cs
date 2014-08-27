// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Game.cs" company="KSS">
//   1997 - 2014
// </copyright>
// <summary>
//   The game class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TopTrumps.Models.Domain
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The game class.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// The players
        /// </summary>
        private readonly List<Player> players; 

        /// <summary>
        /// The deck
        /// </summary>
        private Deck deck;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        public Game()
        {
            this.players = new List<Player> { new Player { Name = "Computer" } };
            this.CardsInPlay = new List<Card>();
        }

        /// <summary>
        /// Gets the deck.
        /// </summary>
        /// <value>
        /// The deck.
        /// </value>
        public Deck Deck
        {
            get
            {
                return this.deck;
            }
        }

        /// <summary>
        /// Gets the players.
        /// </summary>
        /// <value>
        /// The players.
        /// </value>
        public List<Player> Players 
        {
            get
            {
                return this.players;
            }
        }

        /// <summary>
        /// Gets or sets the cards in play.
        /// </summary>
        /// <value>
        /// The cards in play.
        /// </value>
        public List<Card> CardsInPlay { get; set; }

        /// <summary>
        /// Gets a value indicating whether [game over].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [game over]; otherwise, <c>false</c>.
        /// </value>
        public bool GameOver
        {
            get
            {
                return this.Players.Count(player => !player.IsOut) <= 1;
            }
        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        /// <param name="playerName">Name of the player.</param>
        /// <param name="packId">The pack identifier.</param>
        public void Start(string playerName, int packId = 1)
        {
            this.players.Add(new Player { Name = playerName });

            this.deck = new Deck(packId);
            this.deck.Shuffle();

            this.DealCards();
        }

        /// <summary>
        /// Compares the cards.
        /// </summary>
        public void CompareCards()
        {
            var playersInGame = this.Players.Where(player => !player.IsOut).ToList();

            var cardsToCompare = playersInGame.Select(player => player.Hand.First()).ToList();

            var winningValue = cardsToCompare.Max(x => x.Strength);

            var isWinningCard = cardsToCompare.Count(x => x.Strength == winningValue) == 1;

            if (isWinningCard)
            {
                var winningCard = cardsToCompare.First(x => x.Strength == winningValue);
                var winningPlayer = this.Players.First(x => x.Hand.Contains(winningCard));

                this.SetPlayerInControl(winningPlayer);

                this.CollectCards(winningPlayer);
            }
            else
            {
                foreach (var player in playersInGame)
                {
                    var playersCard = player.Hand.First();
                    player.Hand.Remove(playersCard);
                    this.CardsInPlay.Add(playersCard);
                }
            }
        }

        /// <summary>
        /// Deals the cards.
        /// </summary>
        private void DealCards()
        {
            foreach (var player in this.Players)
            {
                player.Hand.Clear();
            }
            
            while (this.deck.Cards.Any())
            {
                foreach (var player in this.Players)
                {
                    if (this.deck.Cards.Any())
                    {
                        player.Hand.Add(this.deck.TakeCard());
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
            foreach (var player in this.Players)
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
            var playersInGame = this.Players.Where(player => !player.IsOut).ToList();
            
            foreach (var player in playersInGame)
            {
                var playersCard = player.Hand.First();
                player.Hand.Remove(playersCard);
                winningPlayer.Hand.Add(playersCard);
            }

            if (this.CardsInPlay.Count > 0)
            {
                winningPlayer.Hand.AddRange(this.CardsInPlay);
                this.CardsInPlay.Clear();
            }
        }
    }
}
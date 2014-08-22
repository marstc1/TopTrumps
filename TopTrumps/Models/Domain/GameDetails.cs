
namespace TopTrumps.Models.Domain
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The Game Details class
    /// </summary>
    public class GameDetails
    {       
        /// <summary>
        /// Initializes a new instance of the <see cref="GameDetails" /> class.
        /// </summary>
        /// <param name="playerName">Name of the player.</param>
        /// <param name="packId">The pack identifier.</param>
        public GameDetails(string playerName = "Guest", int packId = 1)
        {
            this.Players = new List<Player>
                               {
                                   new Player { Id = 0, Name = playerName }, 
                                   new Player { Id = 1, Name = "Computer" }
                               };

            this.Deck = new Deck(packId);
            this.Deck.Shuffle();

            while (Deck.Cards.Any())
            {
                foreach (var player in this.Players)
                {
                    if (Deck.Cards.Any())
                    {
                        player.Hand.Add(Deck.TakeCard());
                    }
                }
            }

            this.CardsInPlay = new List<Card>();
        }

        /// <summary>
        /// Gets or sets the deck.
        /// </summary>
        /// <value>
        /// The deck.
        /// </value>
        public Deck Deck { get; set; }

        /// <summary>
        /// Gets or sets the players.
        /// </summary>
        /// <value>
        /// The players.
        /// </value>
        public List<Player> Players { get; set; }

        /// <summary>
        /// Gets or sets cards in play.
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
                return this.Players.Any(x => x.Hand.Count == 0) && this.CardsInPlay.Count == 0;
            }
        }
    }
}
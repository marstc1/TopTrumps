
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
        /// The deck
        /// </summary>
        private Deck deck;

        /// <summary>
        /// The players
        /// </summary>
        private List<Player> players; 

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        public Game()
        {
            this.players = new List<Player> { new Player { Name = "Computer" } };
            this.CardsInPlay = new List<Card>();
            this.GameOver = false;
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
        /// Gets or sets a value indicating whether [game over].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [game over]; otherwise, <c>false</c>.
        /// </value>
        public bool GameOver { get; set; }

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
            var computersCard = this.Players[0].Hand.FirstOrDefault();
            var playersCard = this.Players[1].Hand.FirstOrDefault();

            if (this.Players.Any(x => x.Hand == null))
            {
                this.GameOver = true;
            }
            else
            {
                this.CardsInPlay.Add(playersCard);
                this.CardsInPlay.Add(computersCard);

                this.Players[1].Hand.Remove(playersCard);
                this.Players[0].Hand.Remove(computersCard);

                if (playersCard.Strength > computersCard.Strength)
                {
                    this.Players[1].Hand.AddRange(this.CardsInPlay);
                    this.CardsInPlay.Clear();
                }

                if (playersCard.Strength < computersCard.Strength)
                {
                    this.Players[0].Hand.AddRange(this.CardsInPlay);
                    this.CardsInPlay.Clear();
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
    }
}
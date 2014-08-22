
namespace TopTrumps.Models.ViewModels.Game
{
    using TopTrumps.Models.Domain;

    /// <summary>
    /// The view model for the game controllers index view
    /// </summary>
    public class GameIndexViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameIndexViewModel"/> class.
        /// </summary>
        public GameIndexViewModel()
        {
            this.PlayerName = "Guest";
            this.PackId = 1;
            this.Difficulty = 0;
        }

        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        /// <value>
        /// The player.
        /// </value>
        public string PlayerName { get; set; }

        /// <summary>
        /// Gets or sets the pack identifier.
        /// </summary>
        /// <value>
        /// The pack identifier.
        /// </value>
        public int PackId { get; set; }

        /// <summary>
        /// Gets or sets the difficulty.
        /// </summary>
        /// <value>
        /// The difficulty.
        /// </value>
        public int Difficulty { get; set; }
    }
}
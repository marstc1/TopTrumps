using Microsoft.Ajax.Utilities;
using TopTrumps.Helpers;

namespace TopTrumps.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using TopTrumps.Models.Domain;
    using TopTrumps.Models.ViewModels;
    using TopTrumps.Models.ViewModels.Game;

    /// <summary>
    /// The Game Controller Class
    /// </summary>
    public class GameController : Controller
    {
        private GameHelper gameHelper;

        public GameController()
        {
            gameHelper = new GameHelper(this.HttpContext);
        }
        /// <summary>
        /// The Game controller Index action method
        /// </summary>
        /// <returns>The landing page for the Game Controller action methods</returns>
        public ActionResult Index()
        {
            var viewModel = new GameIndexViewModel();

            return this.View(viewModel);
        }

        /// <summary>
        /// Indexes the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>The landing page for the Game Controller action methods</returns>
        [HttpPost]
        public ActionResult Index(GameIndexViewModel viewModel)
        {
            var gameDetails = new GameDetails(viewModel.PlayerName, viewModel.PackId);

            gameHelper.SaveGameDetails(gameDetails);

            return this.RedirectToAction("PlayGame");
        }
        
        /// <summary>
        /// Plays the game.
        /// </summary>
        /// <returns>The play game view</returns>
        public ActionResult PlayGame()
        {
            var gameViewModel = new GameViewModel(gameHelper.GetGameDetails());
            
            return this.View(gameViewModel);
        }

        /// <summary>
        /// Plays the game.
        /// </summary>
        /// <param name="gameViewModel">The game view model.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PlayGame(GameViewModel gameViewModel)
        {
            var gameDetails = gameHelper.GetGameDetails();

            var playersCard = gameDetails.Players[0].Hand.FirstOrDefault();
            var computersCard = gameDetails.Players[1].Hand.FirstOrDefault();

            if (playersCard == null && computersCard == null)
            {
                return this.RedirectToAction("GameOver");
            }

            gameDetails.CardsInPlay.Add(playersCard);
            gameDetails.CardsInPlay.Add(computersCard);

            if (computersCard == null || playersCard == null) return this.View(gameViewModel);
            
            if (playersCard.Strength > computersCard.Strength)
            {
                gameDetails.Players[0].Hand.AddRange(gameDetails.CardsInPlay);
                gameDetails.CardsInPlay = new List<Card>();
            }
            
            if (playersCard.Strength < computersCard.Strength)
            {
                gameDetails.Players[1].Hand.AddRange(gameDetails.CardsInPlay);
                gameDetails.CardsInPlay = new List<Card>();
            }

            gameHelper.SaveGameDetails(gameDetails);


            return this.View(gameViewModel);
        }

        /// <summary>
        /// The game over action method.
        /// </summary>
        /// <returns>The game over view.</returns>
        public ActionResult GameOver()
        {
            var gameDetails = gameHelper.GetGameDetails();

            var winner = gameDetails.Players.FirstOrDefault();

            foreach (var player in gameDetails.Players.Where(player => player.Hand.Count > winner.Hand.Count))
            {
                winner = player;
            }

            return this.View(winner);
        }
    }
}

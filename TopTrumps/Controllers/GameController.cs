namespace TopTrumps.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using TopTrumps.Helpers;
    using TopTrumps.Models.Domain;
    using TopTrumps.Models.ViewModels;
    using TopTrumps.Models.ViewModels.Game;

    /// <summary>
    /// The Game Controller Class
    /// </summary>
    public class GameController : Controller
    {
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

            this.SaveGameDetails(gameDetails);

            return this.RedirectToAction("PlayGame");
        }

        [HttpPost]
        public ActionResult NewGame(string selectedOption)
        {
            var httpSessionStateBase = this.HttpContext.Session;
            if (httpSessionStateBase != null)
            {
                var players = (List<Player>)httpSessionStateBase["players"];

                var player1 = players[0];
                var computer = players[1];

                var playersCard = player1.Hand.FirstOrDefault();
                var computersCard = computer.Hand.FirstOrDefault();

                if (playersCard == null || computersCard == null)
                {
                    return this.RedirectToAction("Wins");
                }

                player1.Hand.Remove(playersCard);
                computer.Hand.Remove(computersCard);

                if (playersCard.Strength > computersCard.Strength)
                {
                    player1.Hand.Add(playersCard);
                    player1.Hand.Add(computersCard);
                    this.ViewBag.Message = string.Format("{0} wins", player1.Name);
                }
                else if (playersCard.Strength < computersCard.Strength)
                {
                    computer.Hand.Add(playersCard);
                    computer.Hand.Add(computersCard);
                    this.ViewBag.Message = string.Format("{0} wins", computer.Name);
                }

                httpSessionStateBase["players"] = players;

                var gameViewModel = new GameViewModel(players);
                return View("Game", gameViewModel);
            }

            return this.RedirectToAction("NewGame");
        }

        /// <summary>
        /// Plays the game.
        /// </summary>
        /// <returns>The play game view</returns>
        public ActionResult PlayGame()
        {
            var gameViewModel = new GameViewModel(this.GetGameDetails());
            
            return this.View(gameViewModel);
        }

        [HttpPost]
        public ActionResult PlayGame(GameViewModel gameViewModel)
        {
            var gameDetails = this.GetGameDetails();

            var playersCard = gameDetails.Players[0].Hand.FirstOrDefault();
            var computersCard = gameDetails.Players[1].Hand.FirstOrDefault();

            if (playersCard == null && computersCard == null)
            {
                return this.RedirectToAction("Wins");
            }

            gameDetails.CardsInPlay.Add(playersCard);
            gameDetails.CardsInPlay.Add(computersCard);

            if (computersCard != null && (playersCard != null 
                && playersCard.Strength > computersCard.Strength))
            {
                
            }
            
            return this.View(gameViewModel);
        }

        public ActionResult Wins()
        {
            return this.View();
        }

        /// <summary>
        /// Saves the game details.
        /// </summary>
        /// <param name="gameDetails">The game details.</param>
        private void SaveGameDetails(GameDetails gameDetails)
        {
            if (this.HttpContext.Session != null)
            {
                this.HttpContext.Session["gameDetails"] = gameDetails;
            }
        }

        /// <summary>
        /// Gets the game details.
        /// </summary>
        /// <returns>All previously stored Game Details</returns>
        private GameDetails GetGameDetails()
        {
            if (this.HttpContext.Session != null && this.HttpContext.Session["gameDetails"] != null)
            {
                return (GameDetails)this.HttpContext.Session["gameDetails"];
            }

            return new GameDetails();
        }

        private void CompareCards(string selectedOption)
        {
            var gameDetails = this.GetGameDetails();

            var playersCard = gameDetails.Players[0].Hand.FirstOrDefault();
            var computersCard = gameDetails.Players[1].Hand.FirstOrDefault();

            if (playersCard == null || computersCard == null)
            {
                this.RedirectToAction("Wins");
            }
            else
            {
                if (playersCard.Strength > computersCard.Strength)
                {
                    gameDetails.Players[0].Hand.Add(playersCard);
                    gameDetails.Players[0].Hand.Add(computersCard);
                }
                else if (playersCard.Strength < computersCard.Strength)
                {
                    gameDetails.Players[1].Hand.Add(playersCard);
                    gameDetails.Players[1].Hand.Add(computersCard);
                }
                else
                {
                    gameDetails.TiedCards.Add(playersCard);
                    gameDetails.Players[1].Hand.Add(computersCard);
                }
            }
        }
    }
}

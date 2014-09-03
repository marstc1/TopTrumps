// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameController.cs" company="KSS">
//   1997 - 2014
// </copyright>
// <summary>
//   The game class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TopTrumps.Controllers
{
    using System.Web.Mvc;

    using TopTrumps.Helpers;
    using TopTrumps.Models.Domain;
    using TopTrumps.Models.ViewModels;
    using TopTrumps.Models.ViewModels.Game;

    /// <summary>
    /// The game controller class.
    /// </summary>
    public class GameController : Controller
    {
        /// <summary>
        /// The game
        /// </summary>
        private Game game;

        /// <summary>
        /// The Index view
        /// </summary>
        /// <returns>The view.</returns>
        public ViewResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// News the game.
        /// </summary>
        /// <returns>The view.</returns>
        public ViewResult NewGame()
        {
            this.game = new Game();
            
            var viewModel = new NewGameViewModel();

            return this.View(viewModel);
        }

        /// <summary>
        /// News the game.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>The view.</returns>
        [HttpPost]
        public ActionResult NewGame(NewGameViewModel viewModel)
        {
            if (this.ModelState.IsValid)
            {
                this.game.Start(viewModel.PlayerName);

                return this.RedirectToAction("PlayGame");
            }

            return this.View(viewModel);
        }

        /// <summary>
        /// Plays the game.
        /// </summary>
        /// <returns>The view.</returns>
        public ViewResult PlayGame()
        {
            var gameViewModel = new GameViewModel(this.game.Players);

            return this.View(gameViewModel);
        }

        /// <summary>
        /// Plays the game.
        /// </summary>
        /// <param name="valueToCompare">The value to compare.</param>
        /// <returns>
        /// The view.
        /// </returns>
        [HttpPost]
        public ActionResult PlayGame(string valueToCompare)
        {
            var gameLogicHelper = new GameLogicHelper(this.game);

            gameLogicHelper.CompareCards(valueToCompare.Replace(" ", string.Empty));

            if (this.game.GameOver)
            {
                return this.RedirectToAction("GameOver");
            }

            var gameViewModel = new GameViewModel(this.game.Players);

            return this.View("PlayGame", gameViewModel);
        }

        /// <summary>
        /// Takes the Computers turn.
        /// </summary>
        /// <returns>The PlayGame view after selecting the computers go</returns>
        public ActionResult ComputersTurn()
        {
            var computersChoice = "Strength";

            return this.PlayGame(computersChoice);
        }

        /// <summary>
        /// Games the over.
        /// </summary>
        /// <returns>The view.</returns>
        public ViewResult GameOver()
        {
            return this.View();
        }

        /// <summary>
        /// Called before the action method is invoked.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            this.game = SessionHelper.GetGameData(this.HttpContext);
            
            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// Called after the action method is invoked.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            SessionHelper.SaveGameData(this.HttpContext, this.game);
            
            base.OnActionExecuted(filterContext);
        }
    }
}


namespace TopTrumps.Controllers
{
    using System.Web.Mvc;
    using TopTrumps.Models.Domain;
    using TopTrumps.Models.ViewModels;
    using TopTrumps.Models.ViewModels.Game;

    public class GameController : Controller
    {
        private Game game;
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewGame()
        {
            var viewModel = new NewGameViewModel();

            return this.View(viewModel);
        }

        [HttpPost]
        public ActionResult NewGame(NewGameViewModel viewModel)
        {
            if (this.ModelState.IsValid)
            {
                this.game.Start(viewModel.PlayerName);

                return RedirectToAction("PlayGame");
            }

            return this.View(viewModel);
        }

        public ActionResult PlayGame()
        {
            var gameViewModel = new GameViewModel(this.game.Players);

            return this.View(gameViewModel);
        }

        [HttpPost]
        public ActionResult PlayGame(string selectedOption)
        {
            this.game.CompareCards();

            if (this.game.GameOver)
            {
                return this.RedirectToAction("GameOver");
            }

            var gameViewModel = new GameViewModel(this.game.Players);

            return this.View(gameViewModel);
        }

        public ActionResult GameOver()
        {
            return this.View();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            this.game = this.GetGameData();
            
            base.OnActionExecuting(filterContext);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            this.SaveGameData(game);
            
            base.OnActionExecuted(filterContext);
        }

        private Game SaveGameData(Game gameData)
        {
            if (this.HttpContext.Session != null)
            {
                this.HttpContext.Session["gameData"] = gameData;
            }

            return gameData;
        }

        private Game GetGameData()
        {
            if (this.HttpContext.Session == null)
            {
                return new Game();
            }

            var gameData = (Game)this.HttpContext.Session["gameData"];

            return gameData ?? new Game();
        }
    }
}

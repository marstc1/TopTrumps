
namespace TopTrumps.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using TopTrumps.Models.Domain;
    using TopTrumps.Models.ViewModels;

    public class GameController : Controller
    {
        public GameData GameData { get; set; }
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewGame()
        {
            GameData.Players = new List<Player>
                {
                    new Player { Id = 0, Name = "Guest"},
                    new Player { Id = 1, Name = "Computer" }
                };

            var deck = new Deck(1);
            deck.Shuffle();

            while (deck.Cards.Count() > 0)
            {
                foreach (var player in GameData.Players)
                {
                    if (deck.Cards.Count() > 0)
                    {
                        player.Hand.Add(deck.TakeCard());
                    }
                }
            }

            var gameViewModel = new GameViewModel(GameData.Players);

            return View("Game", gameViewModel);
        }

        [HttpPost]
        public ActionResult NewGame(string selectedOption)
        {
            GameData = this.CompareCard(GameData);

            this.SaveGameData(GameData);

            if (GameData.GameOver)
            {
                return this.RedirectToAction("Wins");
            }

            var gameViewModel = new GameViewModel(GameData.Players);

            return this.View("Game", gameViewModel);
        }

        public ActionResult Wins()
        {
            return this.View();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            this.GameData = this.GetGameData();
            
            base.OnActionExecuting(filterContext);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            this.SaveGameData(GameData);
            
            base.OnActionExecuted(filterContext);
        }

        private GameData CompareCard(GameData gameData)
        {
            var playersCard = gameData.Players[0].Hand.FirstOrDefault();
            var computersCard = gameData.Players[1].Hand.FirstOrDefault();

            if (playersCard == null || computersCard == null)
            {
                gameData.GameOver = true;
            }
            else
            {
                gameData.CardsInPlay.Add(playersCard);
                gameData.CardsInPlay.Add(computersCard);

                gameData.Players[0].Hand.Remove(playersCard);
                gameData.Players[1].Hand.Remove(computersCard);

                if (playersCard.Strength > computersCard.Strength)
                {
                    gameData.Players[0].Hand.AddRange(gameData.CardsInPlay);
                    gameData.CardsInPlay.Clear();
                }

                if (playersCard.Strength < computersCard.Strength)
                {
                    gameData.Players[1].Hand.AddRange(gameData.CardsInPlay);
                    gameData.CardsInPlay.Clear();
                }
            }

            return gameData;
        }

        private GameData SaveGameData(GameData gameData)
        {
            if (this.HttpContext.Session != null)
            {
                this.HttpContext.Session["gameData"] = gameData;
            }

            return gameData;
        }

        private GameData GetGameData()
        {
            if (this.HttpContext.Session == null)
            {
                return new GameData();
            }

            var gameData = (GameData)this.HttpContext.Session["gameData"];

            return gameData ?? new GameData();
        }
    }
}

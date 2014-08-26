
namespace TopTrumps.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using TopTrumps.Models.Domain;
    using TopTrumps.Models.ViewModels;

    public class GameController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewGame()
        {
            var gameData = new GameData();

            gameData.Players = new List<Player>
                {
                    new Player { Id = 0, Name = "Guest"},
                    new Player { Id = 1, Name = "Computer" }
                };

            var deck = new Deck(1);
            deck.Shuffle();

            while (deck.Cards.Count() > 0)
            {
                foreach (var player in gameData.Players)
                {
                    if (deck.Cards.Count() > 0)
                    {
                        player.Hand.Add(deck.TakeCard());
                    }
                }
            }

            this.SaveGameData(gameData);

            var gameViewModel = new GameViewModel(gameData.Players);

            return View("Game", gameViewModel);
        }

        [HttpPost]
        public ActionResult NewGame(string selectedOption)
        {
            var gameData = this.GetGameData();

            gameData = this.CompareCard(gameData);

            this.SaveGameData(gameData);

            if (gameData.GameOver)
            {
                return this.RedirectToAction("Wins");
            }

            var gameViewModel = new GameViewModel(gameData.Players);

            return this.View("Game", gameViewModel);
        }

        public ActionResult Wins()
        {
            return this.View();
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
            if (this.HttpContext.Session != null)
            {
                return (GameData)this.HttpContext.Session["gameData"];
            }

            return new GameData();
        }
    }
}

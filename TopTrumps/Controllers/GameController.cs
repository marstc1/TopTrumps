using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TopTrumps.Helpers;
using TopTrumps.Models.Domain;

namespace TopTrumps.Controllers
{
    using TopTrumps.Models.ViewModels;

    public class GameController : Controller
    {
        //
        // GET: /Game/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewGame()
        {
            var deck = new Deck(1);
            deck.Shuffle();

            var gameData = new Game();

            gameData.Players = new List<Player>
                {
                    new Player { Id = 0, Name = "Guest"},
                    new Player { Id = 1, Name = "Computer" }
                };

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

            var httpSessionStateBase = this.HttpContext.Session;
            if (httpSessionStateBase != null)
            {
                httpSessionStateBase["gameData"] = gameData;
            }

            var gameViewModel = new GameViewModel(gameData.Players);

            return View("Game", gameViewModel);
        }

        [HttpPost]
        public ActionResult NewGame(string selectedOption)
        {
            var httpSessionStateBase = this.HttpContext.Session;
            if (httpSessionStateBase != null)
            {
                var gameData = (Game)httpSessionStateBase["gameData"];

                var players = gameData.Players;

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

                gameData.CardsInPlay.Add(playersCard);
                gameData.CardsInPlay.Add(computersCard);

                if (playersCard.Strength > computersCard.Strength)
                {
                    player1.Hand.AddRange(gameData.CardsInPlay);
                    gameData.CardsInPlay.Clear();
                }
                else if (playersCard.Strength < computersCard.Strength)
                {
                    computer.Hand.AddRange(gameData.CardsInPlay);
                    gameData.CardsInPlay.Clear();
                }

                httpSessionStateBase["gameData"] = gameData;

                var gameViewModel = new GameViewModel(players);
                return View("Game", gameViewModel);
            }

            return this.RedirectToAction("NewGame");
        }

        public ActionResult Wins()
        {
            return this.View();
        }
    }
}

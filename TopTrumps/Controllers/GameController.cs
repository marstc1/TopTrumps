using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TopTrumps.Helpers;
using TopTrumps.Models.Domain;

namespace TopTrumps.Controllers
{
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
            
            var players = new List<Player>
                {
                    new Player { Id = 0, Name = "Guest"},
                    new Player { Id = 1, Name = "Computer" }
                };

            var packHelper = new PackHelper();

            while (deck.Cards.Count() > 0)
            {
                foreach (var player in players)
                {
                    if (deck.Cards.Count() > 0)
                    {
                        player.Hand.Add(deck.TakeCard());
                    }
                }
            }

            this.HttpContext.Session["players"] = players;

            return View(players);
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

                return this.View(players);
            }

            return this.RedirectToAction("NewGame");
        }

        public ActionResult Wins()
        {
            return this.View();
        }
    }
}

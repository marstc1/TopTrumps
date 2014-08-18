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

            if (players[0].Hand.FirstOrDefault().Strength < players[1].Hand.FirstOrDefault().Strength)
            {
                var card = players[0].Hand.FirstOrDefault();
                players[0].Hand.Remove(card);
                players[1].Hand.Add(card);
                ViewBag.Message = string.Format("{1} wins", players[1].Name);
            }
            else if (players[0].Hand.FirstOrDefault().Strength > players[1].Hand.FirstOrDefault().Strength)
            {
                var card = players[1].Hand.FirstOrDefault();
                players[1].Hand.Remove(card);
                players[0].Hand.Add(card);
                ViewBag.Message = string.Format("{0} wins", players[1].Name);
            }
            
            return View(players);
        }
    }
}

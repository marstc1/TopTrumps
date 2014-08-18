using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TopTrumps.Models;
using TopTrumps.Models.Domain;

namespace TopTrumps.Helpers
{
    public class PackHelper
    {
        private TopTrumpsContext db = new TopTrumpsContext();
        
        public void ClearDb()
        {
            System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseAlways<TopTrumps.Models.TopTrumpsContext>());
        }
        
        public void CreatePack() 
        {
            var pack = new Pack();
            pack.Id = 1;
            
            if (!db.Packs.Any(x => x.Id == pack.Id))
            {
                pack.PackName = "Goblins and Faeryfolk";

                db.Packs.Add(pack);
                db.SaveChanges();
            }

            var cards = new List<Card>();
            var cardId = 1;
            var packId = 1;

            cards.Add(new Card { Name = "Balbrigan", CardType = "Goblin", Strength = 11, Skill = 6, MagicalForce = 1, Weapons = 7, Power = 10 });
            cards.Add(new Card { Name = "Bikou", CardType = "Kobold", Strength = 13, Skill = 12, MagicalForce = 3, Weapons = 8, Power = 12 });
            cards.Add(new Card { Name = "Bloodroot", CardType = "Pixie", Strength = 6, Skill = 5, MagicalForce = 8, Weapons = 2, Power = 5 });
            cards.Add(new Card { Name = "Boeth", CardType = "Goblin", Strength = 3, Skill = 9, MagicalForce = 6, Weapons = 1, Power = 5 });
            cards.Add(new Card { Name = "Camas", CardType = "Fawn", Strength = 4, Skill = 3, MagicalForce = 15, Weapons = 0, Power = 15 });

            foreach (var card in cards)
            {
                if (!db.Cards.Any(x => x.Name == card.Name))
                {
                    card.Id = cardId;
                    card.PackId = packId;

                    db.Cards.Add(card);
                    cardId++;
                }
            }

            db.SaveChanges();
        }

        public IEnumerable<Card> DealHand(int numberOfCards)
        {
            var hand = db.Cards.OrderBy(x => Guid.NewGuid()).ToList();

            var cards = hand.Take(numberOfCards);

            var takeCards = cards as Card[] ?? cards.ToArray();
            
            hand.RemoveAll(takeCards.Contains);

            return takeCards;
        }
    }
}
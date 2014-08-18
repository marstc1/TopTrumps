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

            cards.Add(new Card { Name = "Doat", CardType = "Sprite", Strength = 3, Skill = 4, MagicalForce = 3, Weapons = 6, Power = 3 });
            cards.Add(new Card { Name = "Duns Scotus", CardType = "Sprigan", Strength = 4, Skill = 12, MagicalForce = 17, Weapons = 2, Power = 6 });
            cards.Add(new Card { Name = "Dywarth Greid", CardType = "Leprechaun", Strength = 4, Skill = 17, MagicalForce = 16, Weapons = 2, Power = 8 });
            cards.Add(new Card { Name = "Fechner", CardType = "Goblin", Strength = 12, Skill = 7, MagicalForce = 2, Weapons = 4, Power = 7 });
            cards.Add(new Card { Name = "Feigl", CardType = "Goblin", Strength = 8, Skill = 11, MagicalForce = 1, Weapons = 9, Power = 6 });

            cards.Add(new Card { Name = "Fichte", CardType = "Goblin", Strength = 12, Skill = 19, MagicalForce = 16, Weapons = 1, Power = 16 });
            cards.Add(new Card { Name = "Flewdling", CardType = "Goblin", Strength = 10, Skill = 6, MagicalForce = 13, Weapons = 6, Power = 12 });
            cards.Add(new Card { Name = "Frect", CardType = "Goblin", Strength = 4, Skill = 3, MagicalForce = 1, Weapons = 4, Power = 3 });
            cards.Add(new Card { Name = "Geez", CardType = "Goblin", Strength = 9, Skill = 6, MagicalForce = 2, Weapons = 3, Power = 11 });
            cards.Add(new Card { Name = "Greulincx", CardType = "Goblin", Strength = 5, Skill = 11, MagicalForce = 16, Weapons = 2, Power = 15 });

            cards.Add(new Card { Name = "Grotius", CardType = "Goblin", Strength = 6, Skill = 3, MagicalForce = 2, Weapons = 8, Power = 5 });
            cards.Add(new Card { Name = "Grus", CardType = "Goblin", Strength = 7, Skill = 3, MagicalForce = 4, Weapons = 1, Power = 3 });
            cards.Add(new Card { Name = "Haekel", CardType = "Goblin", Strength = 5, Skill = 4, MagicalForce = 6, Weapons = 3, Power = 9 });
            cards.Add(new Card { Name = "Hahn", CardType = "Goblin", Strength = 10, Skill = 4, MagicalForce = 4, Weapons = 3, Power = 4 });
            cards.Add(new Card { Name = "Hobbema", CardType = "Goblin", Strength = 6, Skill = 3, MagicalForce = 2, Weapons = 9, Power = 9 });

            cards.Add(new Card { Name = "Hobeu", CardType = "Goblin", Strength = 12, Skill = 11, MagicalForce = 1, Weapons = 12, Power = 5 });
            cards.Add(new Card { Name = "Hornblende", CardType = "Bogle", Strength = 17, Skill = 3, MagicalForce = 2, Weapons = 0, Power = 1 });
            cards.Add(new Card { Name = "Kes", CardType = "Will 'o' Wisp", Strength = 3, Skill = 4, MagicalForce = 12, Weapons = 2, Power = 3 });
            cards.Add(new Card { Name = "Ludd", CardType = "Brownie", Strength = 4, Skill = 8, MagicalForce = 14, Weapons = 1, Power = 8 });
            cards.Add(new Card { Name = "Moch", CardType = "Goblin", Strength = 5, Skill = 4, MagicalForce = 6, Weapons = 7, Power = 2 });

            cards.Add(new Card { Name = "Nuxvomica", CardType = "Kelpie", Strength = 12, Skill = 12, MagicalForce = 13, Weapons = 1, Power = 11 });
            cards.Add(new Card { Name = "Olibanum", CardType = "Banshee", Strength = 14, Skill = 6, MagicalForce = 3, Weapons = 1, Power = 11 });
            cards.Add(new Card { Name = "Purva Mimosa", CardType = "Water Spirit", Strength = 3, Skill = 18, MagicalForce = 12, Weapons = 0, Power = 12 });
            cards.Add(new Card { Name = "Smilax", CardType = "Trow", Strength = 3, Skill = 3, MagicalForce = 6, Weapons = 2, Power = 3 });
            cards.Add(new Card { Name = "Tragacanth", CardType = "Nymph", Strength = 4, Skill = 8, MagicalForce = 12, Weapons = 4, Power = 7 });

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
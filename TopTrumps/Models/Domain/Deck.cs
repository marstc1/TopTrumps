using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TopTrumps.Models.Domain
{
    public class Deck
    {
        private TopTrumpsContext db = new TopTrumpsContext();

        private List<Card> cards;

        public List<Card> Cards {
            get
            {
                return this.cards;
            }
        }

        public Deck(int packId)
        {
            this.cards = db.Cards.Where(x=> x.PackId == packId).ToList();
        }

        public void Shuffle()
        {
            this.cards = this.cards.OrderBy(x => Guid.NewGuid()).ToList();
        }

        public Card TakeCard()
        {
            var card = cards.FirstOrDefault();

            cards.Remove(card);

            return card;
        }
    }
}
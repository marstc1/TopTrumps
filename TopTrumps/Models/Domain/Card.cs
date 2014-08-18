using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TopTrumps.Models.Domain
{
    public class Card
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string CardType { get; set; }

        public int Strength { get; set; }

        public int Skill { get; set; }

        public int MagicalForce { get; set; }

        public int Weapons { get; set; }

        public int Power { get; set; }

        [ForeignKey("Pack")]
        public int PackId { get; set; }

        public Pack Pack { get; set; }
    }
}
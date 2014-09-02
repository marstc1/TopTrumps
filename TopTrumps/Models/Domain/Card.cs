// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Card.cs" company="KSS">
//   1997 - 2014
// </copyright>
// <summary>
//   The card class
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TopTrumps.Models.Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Card Stat Names
    /// </summary>
    public enum CardStats
    {
        /// <summary>
        /// The strength
        /// </summary>
        Strength,

        /// <summary>
        /// The skill
        /// </summary>
        Skill,

        /// <summary>
        /// The magical force
        /// </summary>
        MagicalForce,

        /// <summary>
        /// The weapons
        /// </summary>
        Weapons,

        /// <summary>
        /// The power
        /// </summary>
        Power
    }

    /// <summary>
    /// The Card class
    /// </summary>
    public class Card
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the card.
        /// </summary>
        /// <value>
        /// The type of the card.
        /// </value>
        public string CardType { get; set; }

        /// <summary>
        /// Gets or sets the strength.
        /// </summary>
        /// <value>
        /// The strength.
        /// </value>
        public int Strength { get; set; }

        /// <summary>
        /// Gets or sets the skill.
        /// </summary>
        /// <value>
        /// The skill.
        /// </value>
        public int Skill { get; set; }

        /// <summary>
        /// Gets or sets the magical force.
        /// </summary>
        /// <value>
        /// The magical force.
        /// </value>
        public int MagicalForce { get; set; }

        /// <summary>
        /// Gets or sets the weapons.
        /// </summary>
        /// <value>
        /// The weapons.
        /// </value>
        public int Weapons { get; set; }

        /// <summary>
        /// Gets or sets the power.
        /// </summary>
        /// <value>
        /// The power.
        /// </value>
        public int Power { get; set; }

        /// <summary>
        /// Gets or sets the pack identifier.
        /// </summary>
        /// <value>
        /// The pack identifier.
        /// </value>
        [ForeignKey("Pack")]
        public int PackId { get; set; }

        /// <summary>
        /// Gets or sets the pack.
        /// </summary>
        /// <value>
        /// The pack.
        /// </value>
        public Pack Pack { get; set; }
    }
}
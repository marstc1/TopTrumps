// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Player.cs" company="KSS">
//   1997 - 2014
// </copyright>
// <summary>
//   Defines the Player type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TopTrumps.Models.Domain
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The player class.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        public Player()
        {
            this.Hand = new List<Card>();
        }
        
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the hand.
        /// </summary>
        /// <value>
        /// The hand.
        /// </value>
        public List<Card> Hand { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is out.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is out; otherwise, <c>false</c>.
        /// </value>
        public bool IsOut
        {
            get
            {
                return !this.Hand.Any();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [in control of game].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [in control of game]; otherwise, <c>false</c>.
        /// </value>
        public bool InControlOfGame { get; set; }
    }
}
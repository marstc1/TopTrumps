﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TopTrumps.Models.Domain
{
    public class Player
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public List<Card> Hand { get; set; }

        public Player()
        {
            this.Hand = new List<Card>();
        }

        public bool IsWinner()
        {
            if (this.Hand.Count() >= 30)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether the player is out.
        /// </summary>
        /// <returns></returns>
        public bool IsOut()
        {
            return !this.Hand.Any();
        }
    }
}
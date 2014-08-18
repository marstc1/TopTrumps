using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace TopTrumps.Models.Domain
{
    public class Pack
    {
        [Key]
        public int Id { get; set; }

        public string PackName { get; set; }

        public List<string> TrumpItems { get; set; }
    }
}

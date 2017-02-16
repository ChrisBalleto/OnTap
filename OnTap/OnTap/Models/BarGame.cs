using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnTap.Models
{
    public class BarGame
    {
        public BarGame()
        {
            this.Bars = new HashSet<Bar>();
        }

        public int Id { get; set; }

        public string GameName { get; set; }

        public virtual ICollection<Bar> Bars { get; set; }
    }
}
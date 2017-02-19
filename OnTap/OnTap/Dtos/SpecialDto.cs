using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using OnTap.Models;

namespace OnTap.Dtos
{
    public class SpecialDto
    {
        public int Id { get; set; }

        public Bar Bar { get; set; }
        public int BarId { get; set; }

        public Models.DayOfWeek DayOfWeek { get; set; }
        public int DayOfWeekId { get; set; }

        public string SpecialDescription { get; set; }
    }
}
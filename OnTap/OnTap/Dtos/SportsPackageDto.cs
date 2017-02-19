using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using OnTap.Models;

namespace OnTap.Dtos
{
    public class SportsPackageDto
    {
        public SportsPackageDto()
        {
            this.Bars = new HashSet<Bar>();
        }

        public int Id { get; set; }

        public string PackageName { get; set; }

        public virtual ICollection<Bar> Bars { get; set; }
    }
}
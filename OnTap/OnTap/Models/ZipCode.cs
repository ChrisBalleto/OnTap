using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnTap.Models
{
    public class ZipCode
    {
        public int Id { get; set; }

        public string ZipCodeNum { get; set; }
    }
}
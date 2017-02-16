using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnTap.Models;
using System.ComponentModel.DataAnnotations;

namespace OnTap.ViewModels
{
    public class RegisterPatronViewModel
    {
        public Patron Patron { get; set; }
        public IEnumerable<RoleName> RoleNames { get; set; }
        public IEnumerable<City> Cities { get; set; }
        public IEnumerable<ZipCode> ZipCodes { get; set; }
        public IEnumerable<State> States { get; set; }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnTap.Models;
using System.ComponentModel.DataAnnotations;

namespace OnTap.ViewModels
{
    public class RoleViewModel
    {
        public IEnumerable<RoleName> RoleNames { get; set; }
        public RoleName RoleName { get; set; }

        [Display(Name = "User Type")]
        public int RoleNameId { get; set; }
    }
}
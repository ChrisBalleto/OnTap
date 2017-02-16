using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnTap.Models;

namespace OnTap.ViewModels
{
    public class RoleViewModel
    {
        public IEnumerable<RoleName> RoleNames { get; set; }
        public RoleName RoleName { get; set; }
        public int RoleNameId { get; set; }
    }
}
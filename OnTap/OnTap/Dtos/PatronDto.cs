using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using OnTap.Models;

namespace OnTap.Dtos
{
    public class PatronDto
    {
        public PatronDto()
        {
            FollowedBars = new List<Bar>();
            BarReviews = new List<Review>();
        }
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email/Username")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

       public int RoleNameId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Min21ToSignUp]
        public DateTime? BirthDate { get; set; }
    
        public string StreetOne { get; set; }

        public string StreetTwo { get; set; }

        public int CityId { get; set; }

        public int StateId { get; set; }

        public int ZipCodeId { get; set; }

        public string PhoneNumber { get; set; }

        public string Geolocation { get; set; }

        public virtual ICollection<Bar> FollowedBars { get; set; }
        public virtual ICollection<Review> BarReviews { get; set; }
    }
}
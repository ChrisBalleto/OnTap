using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnTap.Models
{
    public class Patron
    {
        public Patron()
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

        public RoleName RoleName { get; set; }
        public int RoleNameId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Date of Birth MM/DD/YYYYY")]
        [Min21ToSignUp]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Street One")]
        public string StreetOne { get; set; }

        [Display(Name = "Street Two")]
        public string StreetTwo { get; set; }

        
        public City City { get; set; }
        [Display(Name = "City")]
        public int CityId { get; set; }

        
        public State State { get; set; }
        [Display(Name = "State")]
        public int StateId { get; set; }

        
        public ZipCode ZipCode { get; set; }
        [Display(Name = "Zip-Code")]
        public int ZipCodeId { get; set; }

        public string ParsedAddress { get; set; }

        [Display(Name = "Phone Number (optional)")]
        public string PhoneNumber { get; set; }

        
        public string Geolocation { get; set; }

        public string GetParsedAddress
        {
            get { return (StreetOne + StreetTwo + ",+").Replace(" ", "+"); }
        }

        public virtual ICollection<Bar> FollowedBars { get; set; }
        public virtual ICollection<Review> BarReviews { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnTap.Models
{
    public class Bar
    {
        public Bar()
        {
            HoursOfOperations = new List<HoursOfOperation>();
            BarReviews = new List<Review>();
            FeedMessages = new List<FeedMessage>();
            Specials = new List<Special>();
            Followers = new List<Patron>();
            this.BarGames = new HashSet<BarGame>();
            this.TapBeers = new HashSet<TapBeer>();
            this.SportsPackages = new HashSet<SportsPackage>();
        }

        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
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

        [Display(Name = "Bar Description")]
        public string BarDescription { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public RoleName RoleName { get; set; }
        public int RoleNameId { get; set; }

        [Display(Name = "Bar Name")]
        public string BarName { get; set; }

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

        public bool HasWifi { get; set; }

        public bool HasJukebox { get; set; }

        public double Rating { get; set; }

        public int? RatingCount { get; set; }

        public List<string> BeerIdList { get; set; }

        public string ParsedAddress { get; set; }

        public string GetParsedAddress
        {
            get { return (StreetOne + StreetTwo + ",+").Replace(" ", "+").ToString(); }
        }

        public IEnumerable<FeedMessage> GetFeedMessages
        {
            get { return FeedMessages.OrderBy(t => t.Created); }
        }

        public IEnumerable<Special> GetSpecials
        {
            get { return Specials.OrderBy(t => t.DayOfWeekId); }
        }

        public string GetJukeboxYesNo
        {
            get { return this.HasJukebox ? "Yes" : "No"; }
        }

        public string GetWifiYesNo
        {
            get { return this.HasWifi ? "Yes" : "No"; }
        }

        public virtual ICollection<HoursOfOperation> HoursOfOperations { get; set; }
        public virtual ICollection<Special> Specials { get; set; }
        public virtual ICollection<Review> BarReviews { get; set; }
        public virtual ICollection<FeedMessage> FeedMessages { get; set; }
        public virtual ICollection<BarGame> BarGames { get; set; }
        public virtual ICollection<TapBeer> TapBeers { get; set; }
        public virtual ICollection<SportsPackage> SportsPackages { get; set; }
        public virtual ICollection<Patron> Followers { get; set; }



    }
}
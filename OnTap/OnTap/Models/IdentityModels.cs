using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace OnTap.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Bar> Bars { get; set; }
        public DbSet<BarGame> BarGames { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<ZipCode> ZipCodes { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<DayOfWeek> DayOfWeeks { get; set; }
        public DbSet<FeedMessage> FeedMessages { get; set; }
        public DbSet<HoursOfOperation> HoursOfOperations { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Special> Specials { get; set; }
        public DbSet<SportsPackage> SportsPackages { get; set; }
        public DbSet<TapBeer> TapBeers { get; set; }
        public DbSet<Patron> Patrons { get; set; }
        public DbSet<RoleName> RoleNames { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
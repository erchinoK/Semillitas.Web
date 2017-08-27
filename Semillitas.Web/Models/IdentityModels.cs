using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System;

namespace Semillitas.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        /*title, gender, dob, address, post code, phone, marketing, T&C
         * 
         * Address, PostCode, Email, PhoneNumber
         * */
        //public string Title { get; set; }
        //public string Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        //public bool IsActive { get; set; }
        //public string Status { get; set;  }
        //public bool AllowMarketing { get; set; }
        //public string Notes { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }   // Stores the memberships associated to the user (in case of having many children)

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public static implicit operator ApplicationUser(Task<ApplicationUser> v)
        {
            throw new NotImplementedException();
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        //public System.Data.Entity.DbSet<Semillitas.Web.Models.Membership> Memberships { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }


}
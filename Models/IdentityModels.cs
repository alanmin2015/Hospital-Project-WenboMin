﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace hospital_project.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
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
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        // ============ ADD MODELS BELOW SO WHEN MIGRATING THEY BECOME TABLES ============
        // public DbSet<ModelNameSingular> ModelNamePlural { get; set;}
        //Physicians Table
        public DbSet<Physician> Physicians { get; set; }
        //Departments Table
        public DbSet<Department> Departments { get; set; }
        //Volunteers Table
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<Researcher> Researchers { get; set; }
        public DbSet<Lab> Labs { get; set; }
        public DbSet<Project> Projects { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
using ManageStudent1.Extentions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageStudent1.Models
{
    public class AppDbContext : IdentityDbContext<AppUser> // changement de DbContext , ajout new type <AppUser> pour accept imigration apres modification sur IdentityUser properties 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {
            

        }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) // si on veut initialiser la base de donnee et inserer des enregistrement 
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }
















    }
}

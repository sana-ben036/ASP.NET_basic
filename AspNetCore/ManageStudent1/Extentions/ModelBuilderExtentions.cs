using ManageStudent1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageStudent1.Extentions
{
    public static class ModelBuilderExtentions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
                new Student()
                {
                    CIN = "ha123",
                    IsActive = true,
                    Prenom = "sana",
                    Nom = "ben",
                    Adresse = "Youssoufia",
                    Filiere = Filiere.CSharp
                },
                 new Student()
                 {
                     CIN = "ha456",
                     IsActive = false,
                     Prenom = "samir",
                     Nom = "salman",
                     Adresse = "Safi",
                     Filiere = Filiere.J2EE
                 },
                 new Student()
                 {
                     CIN = "ha111",
                     IsActive = false,
                     Prenom = "karim",
                     Nom = "fahmi",
                     Adresse = "Safi",
                     Filiere = Filiere.J2EE
                 }
                 );

        }
    }
}

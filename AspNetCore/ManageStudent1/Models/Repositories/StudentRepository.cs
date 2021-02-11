using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ManageStudent1.Models.Repositories
{
    public class StudentRepository : ICompanyRepository<Student>
    {
        public List<Student> Students;
        public IConfiguration Configuration { get; }

        public StudentRepository(IConfiguration configuration)
        {
            Configuration = configuration;

            Students = new List<Student>
            {
                new Student() { CIN = "ha123", IsActive = true, Prenom = "sana", Nom = "ben", Adresse = "Youssoufia", Filiere = Filiere.CSharp },
                new Student() { CIN = "ha456", IsActive = false, Prenom = "swino", Nom = "beng", Adresse = "Safi", Filiere = Filiere.J2EE }
            };
        }

        public IEnumerable<Student> GetList()
        {
            
            return Students;
        }

        public Student Get(string cin)
        {
            return Students.SingleOrDefault(s => s.CIN == cin);
        }
        public void Add(Student student)
        {
            Students.Add(student);
        }

        public Student Delete(string cin)
        {
            var student = Students.Find(s => s.CIN == cin);
            if (student != null)
            {
                Students.Remove(student);
            }
            return student;
        }

        public Student Update(Student entityChanges)
        {
            var student = Students.Find(s => s.CIN == entityChanges.CIN);
            if (student != null)
            {
                student.CIN = entityChanges.CIN;
                student.IsActive = entityChanges.IsActive;
                student.Prenom = entityChanges.Prenom;
                student.Nom = entityChanges.Nom;
                student.Adresse = entityChanges.Adresse;
                student.Filiere = entityChanges.Filiere;
            }
            return student;
        }
    }
}

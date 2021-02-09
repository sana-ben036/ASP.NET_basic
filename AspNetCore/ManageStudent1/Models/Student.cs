using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace ManageStudent1.Models
{
    public class Student
    {
        public int CIN { get; set; }
        public bool IsActive { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public string Filiere { get; set; } //  public Filiere Filiere { get; set; } object



    }
}

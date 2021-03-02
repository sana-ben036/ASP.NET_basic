using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;



namespace ManageStudent1.Models
{
    public class Student
    {
        [Required]
        [MinLength (5,ErrorMessage = " CIN doit contenir au minimum 5 caractéres")]
        [MaxLength (8)]
        [Key]
        public string CIN { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public string Prenom { get; set; }
        [Required]
        public string Nom { get; set; }

        [Required]
        public string Adresse { get; set; }
        [Required]
        public Filiere? Filiere { get; set; } //  public string Filiere { get; set; } 



    }
}

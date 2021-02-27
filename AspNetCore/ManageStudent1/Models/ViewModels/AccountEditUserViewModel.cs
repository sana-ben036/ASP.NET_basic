using ManageStudent1.Tools;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ManageStudent1.Models.ViewModels
{
    public class AccountEditUserViewModel
    {
        public AccountEditUserViewModel()
        {
            Roles = new List<string>();
            Claims = new List<string>();
        }


        public string Id { get; set; }


        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [ValidEmailDomainAttribut(Domain: "gmail.com", ErrorMessage = "Email Domain must be gmail.com")]
        public string Email { get; set; }

        public int Age { get; set; }


        public IList<string> Roles { get; set; }
        public IList<string> Claims { get; set; }























    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageStudent1.Models
{
    public class AppUser : IdentityUser
    {
        public int Age { get; set; }
        public string CIN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}

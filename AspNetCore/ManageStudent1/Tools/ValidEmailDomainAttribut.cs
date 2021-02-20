using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManageStudent1.Tools
{
    public class ValidEmailDomainAttribut : ValidationAttribute
    {
        private string Domain { get; set; }

        public ValidEmailDomainAttribut(string Domain)
        {
            this.Domain = Domain;
        }

        public override bool IsValid(object value)
        {
            string [] values = value.ToString().Split("@");
            if(values[1].ToLower() == this.Domain.ToLower())
            {
                return true;
            }
            else
            {
                return false;
            }
        }






    }
}

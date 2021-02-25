using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageStudent1.Models.ViewModels
{
    public class EditRoleViewModel : Role
    {
        public string Id { get; set; }

        public List<string> Users { get; set; }
    }
}

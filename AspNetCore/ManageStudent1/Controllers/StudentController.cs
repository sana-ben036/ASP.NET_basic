using ManageStudent1.Models;
using ManageStudent1.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageStudent1.Controllers
{
    public class StudentController : Controller
    {
        
        private ICompanyRepository<Student> _companyRepository;

        public StudentController(ICompanyRepository<Student> companyRepository)
        {
            _companyRepository = companyRepository;
           
        }

        public ViewResult List()
        {
            List<Student> Students = _companyRepository.GetAll();
            

            return View();

        }

        public ViewResult Add()
        {


            return View();

        }

        public ViewResult Edit()
        {


            return View();

        }

        public ViewResult Delete()
        {


            return View();

        }








    }
}

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
            List<Student> Students = _companyRepository.Get();
            

            return View(Students);

        }

        [HttpGet]
        public ViewResult AddStudent()
        {

            return View();

        }
        [HttpPost]
        public ViewResult AddStudent(Student student)
        {

            _companyRepository.Add(student);

            return View("List");

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

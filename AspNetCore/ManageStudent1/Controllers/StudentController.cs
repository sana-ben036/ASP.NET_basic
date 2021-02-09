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

        public ActionResult Search(string id)
        {
            Student student = _companyRepository.Get(id);


            return View(student);

        }

        public ActionResult List()
        {
            IEnumerable<Student> Students = _companyRepository.GetList();
            

            return View(Students);

        }

        [HttpGet]
        public ActionResult AddStudent()
        {

            return View();

        }
        [HttpPost]
        public ActionResult AddStudent(Student student)
        {

            _companyRepository.Add(student);

            return RedirectToAction("List");

        }

        public ActionResult EditStudent()
        {


            return RedirectToAction("List");

        }

        public ActionResult DeleteStudent()
        {


            return RedirectToAction("List");

        }








    }
}

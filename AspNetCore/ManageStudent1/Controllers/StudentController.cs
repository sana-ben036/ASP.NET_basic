using ManageStudent1.Models;
using ManageStudent1.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ManageStudent1.Controllers
{
    [Authorize (Roles = "Staf,IT,Admin,Student")]
    public class StudentController : Controller
    {
        

        private readonly ICompanyRepository<Student> _companyRepository;

        public StudentController(ICompanyRepository<Student> companyRepository)
        {
            _companyRepository = companyRepository;
           
        }


        
        public ActionResult Search(string id)
        {
            if(id is null)
            {
                return RedirectToAction("index");
            }
            Student student = _companyRepository.Get(id );
            if (student is null)
            {
                return View("../Errors/NotFound", $"The Student with CIN : {id} cannot be found");
            }

            return View(student);

        }


        [AllowAnonymous]
        public ActionResult Index()
        {
            IEnumerable<Student> Students = _companyRepository.GetList();


            return View(Students);
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
        public ActionResult AddStudent(Student model)
        {
            if (ModelState.IsValid && (_companyRepository.Get(model.CIN) == null))
            {
                Student student = new Student()
                {
                    CIN = model.CIN,
                    IsActive = model.IsActive,
                    Prenom = model.Prenom,
                    Nom = model.Nom,
                    Adresse = model.Adresse,
                    Filiere = model.Filiere

                };

                _companyRepository.Add(student);

                return RedirectToAction("Search", new { id = student.CIN });
                

            }
            ViewBag.Pk = "CIN est déja exist !!";
            return View(model);

        }

        [HttpGet]
        public ActionResult EditStudent(string id)
        {
            Student student = _companyRepository.Get(id);
            if (student is null)
            {
                return View("../Errors/NotFound", $"The Student with  CIN : {id} cannot be found");
            }
            Student model = new Student()
            {
                CIN = student.CIN,
                IsActive = student.IsActive,
                Prenom = student.Prenom,
                Nom = student.Nom,
                Adresse = student.Adresse,
                Filiere = student.Filiere

            };

            return View(model);

        }

        
        [HttpPost]
        public ActionResult EditStudent(Student model)
        {
            if (ModelState.IsValid)
            {
              Student student = _companyRepository.Get(model.CIN);

                //student.CIN = model.CIN;
                student.IsActive = model.IsActive;
                student.Prenom = model.Prenom;
                student.Nom = model.Nom;
                student.Adresse = model.Adresse;
                student.Filiere = model.Filiere;
            
                
                _companyRepository.Edit(student);
                return RedirectToAction("Search", new { id = student.CIN });


            }
            
            return View(model);

        }


        [HttpPost]
        public ActionResult DeleteStudent(string id)
        {
            Student student = _companyRepository.Get(id);
            if (student is null)
            {
                return View("../Errors/NotFound", $"The Student with  CIN : {id} cannot be found");
            }

            _companyRepository.Delete(student.CIN);

            return RedirectToAction("List");

        }








    }
}

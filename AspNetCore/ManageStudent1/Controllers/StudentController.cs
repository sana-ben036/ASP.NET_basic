using ManageStudent1.Models;
using ManageStudent1.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManageStudent1.Controllers
{
    [Authorize (Roles = "Staf,IT,Admin,Student")]
    public class StudentController : Controller
    {
        

        private readonly ICompanyRepository<Student> _companyRepository;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public StudentController(ICompanyRepository<Student> companyRepository , UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _companyRepository = companyRepository;
            this.userManager = userManager;
            this.signInManager = signInManager;


        }

        


        public async Task<IActionResult> Profil(string id)
        {
            if(id is null)
            {
                return RedirectToAction("index");
            }
            else if(signInManager.IsSignedIn(User) && User.IsInRole("Student")) //pour eviter de modifier CIN in url et avoir les info d'un autre etudiant
            {
                AppUser user = await userManager.FindByEmailAsync(User.Identity.Name);
                var userCin = user.CIN;

                Student student = _companyRepository.Get(userCin);

                //if (userCin == id)
                //{
                //    Student student = _companyRepository.Get(id);
                //    if (student is null)
                //    {
                //        return View("../Error/NotFound", $"The Student with CIN : {id} cannot be found");
                //    }
                //    return View(student);
                //}

                //return RedirectToAction("AccessDenied", "Account");

                return View(student);
            }
            else
            {
                Student student = _companyRepository.Get(id);
                if (student is null)
                {
                    return View("../Error/NotFound", $"The Student with CIN : {id} cannot be found");
                }
                return View(student);
            }


            //return RedirectToAction("index", "Student");

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

                return RedirectToAction("List"); //return RedirectToAction("Search", new { id = student.CIN });


            }
            ViewBag.Pk = "CIN est déja exist !!";
            return View(model);

        }

        [HttpGet]
        public async Task<ActionResult> EditStudent(string id)
        {
            
            if (signInManager.IsSignedIn(User) && User.IsInRole("Student")) // pour eviter de modifier CIN in url et avoir les info d'un autre etudiant
            {
                AppUser user = await userManager.FindByEmailAsync(User.Identity.Name);
                var userCin = user.CIN;

                Student student = _companyRepository.Get(userCin);

                if (student is null)
                {
                    return View("../Error/NotFound", $"The Student with  CIN : {id} cannot be found");
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
            else
            {
                Student student = _companyRepository.Get(id);
                if (student is null)
                {
                    return View("../Error/NotFound", $"The Student with  CIN : {id} cannot be found");
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
                return RedirectToAction("index");  //return RedirectToAction("Search", new { id = student.CIN });


            }
            
            return View(model);

        }


        [HttpPost]
        public ActionResult DeleteStudent(string id)
        {
            Student student = _companyRepository.Get(id);
            if (student is null)
            {
                return View("../Error/NotFound", $"The Student with  CIN : {id} cannot be found");
            }

            _companyRepository.Delete(student.CIN);

            return RedirectToAction("List");

        }








    }
}

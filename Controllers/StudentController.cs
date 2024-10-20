using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using WebApplication3.Models.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication3.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class StudentController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly ISchoolRepository schoolRepository;

        public StudentController(IStudentRepository studentRepository, ISchoolRepository schoolRepository)
        {
            this.studentRepository = studentRepository;
            this.schoolRepository = schoolRepository;
        }


        // GET: StudentController
        [AllowAnonymous]
        public IActionResult Index()
{
    var students = studentRepository.GetAll();  // Récupère tous les étudiants
    if (students == null) 
    {
        return View(new List<Student>());  // Retourne une liste vide pour éviter un NullReferenceException
    }

    return View(students);
}


        // GET: StudentController/Create
        public ActionResult Create()
        {
            ViewBag.SchoolID = new SelectList(schoolRepository.GetAll(), "SchoolID", "SchoolName");
            return View();
        } 


        // Details
        public ActionResult Details(int id)
        {
            var Student = studentRepository.GetById(id);

            return View(Student);
        }



        // POST: StudentController/Create
        // GET: StudentController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var student = studentRepository.GetById(id);

                ViewBag.SchoolID = new SelectList(schoolRepository.GetAll(), "SchoolID", "SchoolName", student.SchoolID);
                return View(student);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    studentRepository.Edit(student);
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.SchoolID = new SelectList(schoolRepository.GetAll(), "SchoolID", "SchoolName", student.SchoolID);
                return View(student);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        public ActionResult Search(string name, int? schoolid)

        {
            var result = studentRepository.GetAll();
            if (!string.IsNullOrEmpty(name))
                result = studentRepository.FindByName(name);
            else
            if (schoolid != null)
                result = studentRepository.GetStudentsBySchoolID(schoolid);
            ViewBag.SchoolID = new SelectList(schoolRepository.GetAll(), "SchoolID", "SchoolName");

            return View("Index", result);
        }
    }
}
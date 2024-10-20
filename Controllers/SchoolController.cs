using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using WebApplication3.Models.Repositories;

namespace WebApplication3.Controllers
{
    
    [Authorize(Roles = "Admin,Manager")]
    public class SchoolController : Controller
    {
        private readonly ISchoolRepository _schoolRepository;

        public SchoolController(ISchoolRepository schoolRepository)
        {
            _schoolRepository = schoolRepository;
        }

        // GET: SchoolController/Index
        [AllowAnonymous]
        public IActionResult Index()
        {
            var schools = _schoolRepository.GetAll();
            return View(schools);
        }

        // GET: SchoolController/Details/5
        public IActionResult Details(int id)
        {
            var school = _schoolRepository.GetById(id);
            if (school == null)
            {
                return NotFound();
            }
            return View(school);
        }

        // GET: SchoolController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SchoolController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(School school)
        {
            try
            {
                _schoolRepository.Add(school);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            { 
                return View(school);
            }
        }

        // GET: SchoolController/Edit/5
        public IActionResult Edit(int id)
        {
            var school = _schoolRepository.GetById(id);
            if (school == null)
            {
                return NotFound();
            }
            return View(school);
        }

        // POST: SchoolController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(School school)
        {
            if (ModelState.IsValid)
            {
                _schoolRepository.Edit(school);
                return RedirectToAction(nameof(Index));
            }
            return View(school);
        }

        // GET: SchoolController/Delete/5
        public IActionResult Delete(int id)
        {
            var school = _schoolRepository.GetById(id);
            if (school == null)
            {
                return NotFound();
            }
            return View(school);
        }

        // POST: SchoolController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(School school)
        {
            _schoolRepository.Delete(school);
            return RedirectToAction(nameof(Index));
        }
    }
}

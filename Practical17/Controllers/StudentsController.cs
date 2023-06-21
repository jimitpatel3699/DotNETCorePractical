using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Practical17.Data;
using Practical17.Models;
using Practical17.Repository;

namespace Practical17.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        public StudentsController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        // GET: Students
        [Authorize]
        public IActionResult Index()
        {
            var model = _studentRepository.GetAllStudents();
            return View(model);
        }

        // GET: Students/Details/5
        [Authorize]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = _studentRepository.GetStudent(id??1);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create(Student student)
        {
            if(DateTime.Now.Year-student.DOB.Year<6)
            {
                ModelState.AddModelError("invalid DOB", "Student age must greater 6 year.");
            }
            if (ModelState.IsValid)
            {
                student.Age = DateTime.Now.Year - student.DOB.Year;
                _studentRepository.Add(student);
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        [Authorize]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = _studentRepository.GetStudent(id ??1);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Edit(Student student)
        {
            if (DateTime.Now.Year - student.DOB.Year < 6)
            {
                ModelState.AddModelError("invalid DOB", "Student age must greater 6 year.");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    student.Age = DateTime.Now.Year - student.DOB.Year;
                    _studentRepository.Update(student);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Delete/5
        [Authorize]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = _studentRepository.GetStudent(id ?? 1);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult DeleteConfirmed(int id)
        {
            if (id != null)
            {
                _studentRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            return View();
            
        }
        [NonAction]
        private bool StudentExists(int id)
        {
          var student = _studentRepository.GetStudent(id);
          if(student!=null)
            {
                return true;
            }
            return false;
        }
    }
}

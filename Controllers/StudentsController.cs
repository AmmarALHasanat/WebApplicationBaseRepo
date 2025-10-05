using Microsoft.AspNetCore.Mvc;
using WebApplicationBaseRepo.Models;
using WebApplicationBaseRepo.Repositories;

namespace WebApplicationBaseRepo.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentRepository _repository;
        private readonly StandardRepository _standardRepository;

        public StudentsController(StudentRepository repository, StandardRepository standardRepository)
        {
            _repository = repository;
            _standardRepository = standardRepository;
        }
        public async Task<IActionResult> Index(StudentSearchViewModel viewModel)
        {

            var students = await _repository.GetAll(s =>
                (string.IsNullOrEmpty(viewModel.Name) || s.FirstName.Contains(viewModel.Name)) &&
                (!viewModel.MinHeight.HasValue || s.Height >= viewModel.MinHeight.Value) &&
                (!viewModel.MaxHeight.HasValue || s.Height <= viewModel.MaxHeight.Value)
            );

            viewModel.Students = students;

            return View(viewModel);
        }
        public async Task<IActionResult> Details(int id)
        {
            var student = await _repository.GetById(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        public async Task<IActionResult> Create()
        {

            ViewBag.standards = await _standardRepository.GetAll() ?? new List<Standard>();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                var result = await _repository.Add(student);
                if (result)
                {
                    TempData["msg"] = "Student Saved Successfully...";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the student.");
                }
            }
            return View(student);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var student = await _repository.GetById(id);
            if (student is null)
            {
                return NotFound();
            }
            ViewBag.standards = await _standardRepository.GetAll() ?? new List<Standard>();

            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                var result = await _repository.Update(student);
                if (result)
                {
                    TempData["msg"] = "Student Updated Successfully...";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the student.");
                }
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _repository.Delete(id);
            if (result)
            {
                TempData["msg"] = "Student Deleted Successfully...";
            }
            else
            {
                TempData["err"] = "An error occurred while deleting the student.";
            }
            return RedirectToAction("Index");
        }
    }
}

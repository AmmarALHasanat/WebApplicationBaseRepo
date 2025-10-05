using Microsoft.AspNetCore.Mvc;
using WebApplicationBaseRepo.Models;
using WebApplicationBaseRepo.Repositories;

namespace WebApplicationBaseRepo.Controllers
{
    public class StandardsController : Controller
    {
        private readonly StandardRepository _repository;
        public StandardsController(StandardRepository repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> Index(string? text)
        {
            var standards = await _repository.GetAll(s => string.IsNullOrEmpty(text) || s.StandardName.Contains(text) || s.Description.Contains(text));
            return View(standards);
        }

        public async Task<IActionResult> Details(int id)
        {
            var standard = await _repository.GetByIdWithStudents(id);
            if (standard == null)
            {
                return NotFound();
            }

            return View(standard);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Standard standard)
        {
            if (ModelState.IsValid)
            {
                var result = await _repository.Add(standard);
                if (result)
                {
                    TempData["msg"] = "Standard Saved Successfully...";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the standard.");
                }
            }
            return View(standard);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var standard = await _repository.GetById(id);
            if (standard == null)
            {
                return NotFound();
            }
            return View(standard);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Standard standard)
        {
            if (ModelState.IsValid)
            {
                var result = await _repository.Update(standard);
                if (result)
                {
                    TempData["msg"] = "Standard Updated Successfully...";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the standard.");
                }
            }
            return View(standard);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _repository.Delete(id);
            if (result)
            {
                TempData["msg"] = "standard Deleted Successfully...";
            }
            else
            {
                TempData["err"] = "An error occurred while deleting the standard.";
            }
            return RedirectToAction("Index");
        }

        //https://localhost:7091/Standards/1/UnlinkStudent/303

        [HttpPost("Standards/{id:int}/UnlinkStudent/{studentId:int}")]
        public async Task<IActionResult> removeStudent(int id, int studentId)
        {
            bool result = await _repository.UnlinkStudent(id, studentId);
            if (result)
            {
                TempData["msg"] = "reomved student for standard Successfully...";
            }
            else
            {
                TempData["err"] = "An error occurred while reomving the student.";
            }
            return RedirectToAction("Details", new { id });
        }
    }
}

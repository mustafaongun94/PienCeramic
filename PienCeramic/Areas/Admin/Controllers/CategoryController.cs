using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PienCeramic.Data;
using PienCeramic.DataAccess.Repository.IRepository;
using PienCeramic.Models;
using PienCeramic.Models.ViewModels;
using PienCeramic.Utility;

namespace PienCeramic.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)] 
    public class CategoryController : Controller
    {
        private readonly IUnitofWork _unitofWork;

        public CategoryController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitofWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            //if (obj.Name == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("name", "Display Order ile category Name aynı olamaz.");
            //}

            
            if (ModelState.IsValid)
            {
                _unitofWork.Category.Add(obj);
                _unitofWork.Save();
                TempData["success"] = "Kategori bilgileri başarıyla eklendi.";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();

            }
            Category categoryFromDb = _unitofWork.Category.Get(c => c.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);

        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitofWork.Category.Update(obj);
                _unitofWork.Save();
                TempData["success"] = "Kategori bilgileri başarıyla güncellendi";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();

            }
            Category categoryFromDb = _unitofWork.Category.Get(c => c.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _unitofWork.Category.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitofWork.Category.Remove(obj);
            _unitofWork.Save();
            TempData["success"] = "Kategori silme işlemi tamamlandı.";
            return RedirectToAction("Index");
        }
    }
}

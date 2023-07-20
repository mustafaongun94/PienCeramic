using Microsoft.AspNetCore.Mvc;
using PienCeramic.Data;
using PienCeramic.Models;

namespace PienCeramic.Controllers
{   
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;   
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
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
            if (ModelState.IsValid) { 
            _db.Categories.Add(obj);
            _db.SaveChanges();
            TempData["success"] = "Kategori bilgileri başarıyla eklendi.";
            return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {   
            if(id==null || id==0 )
            {   
                    return NotFound();
                
            }
            Category categoryFromDb = _db.Categories.FirstOrDefault(c => c.Id == id); 
            if(categoryFromDb == null)
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
                _db.Categories.Update(obj);
                _db.SaveChanges();
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
            Category categoryFromDb = _db.Categories.FirstOrDefault(c => c.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Kategori silme işlemi tamamlandı.";
            return RedirectToAction("Index");
        }
    }
}

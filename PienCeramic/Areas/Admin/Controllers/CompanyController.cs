using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PienCeramic.DataAccess.Repository.IRepository;
using PienCeramic.Models;
using PienCeramic.Models.ViewModels;
using PienCeramic.Utility;

namespace PienCeramic.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitofWork _unitofWork;
     
        public CompanyController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            List<Company> objCompanyList = _unitofWork.Company.GetAll().ToList();
            return View(objCompanyList);
        }

        public IActionResult Upsert(int? id)
        {
            
            
            if (id == null || id == 0)
            {
                return View(new Company());
            }
            else
            {
                Company companyObj = _unitofWork.Company.Get(u => u.Id == id);
                return View(companyObj);
            }


        }
        [HttpPost]
        public IActionResult Upsert(Company companyObj)
        {
            if (ModelState.IsValid)
            {
                
               
                if (companyObj.Id == 0)
                {
                    _unitofWork.Company.Add(companyObj);
                }
                else
                {
                    _unitofWork.Company.Update(companyObj);
                }
                _unitofWork.Save();
                TempData["success"] = "Kategori bilgileri başarıyla eklendi.";
                return RedirectToAction("Index");
            }
            else
            {
                return View(companyObj);
            }

        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objCompanyList = _unitofWork.Company.GetAll().ToList();
            return Json(new { data = objCompanyList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            {
                var CompanyToBeDeleted = _unitofWork.Company.Get(u => u.Id == id);
                if (CompanyToBeDeleted == null) { return Json(new { success = false, message = "Silme işleminde hata oluştu" }); }
                
                _unitofWork.Company.Remove(CompanyToBeDeleted);
                _unitofWork.Save();

                return Json(new { success = true, message = "Silme işlemi başarılı" });
            }

            #endregion
        }
    }
}

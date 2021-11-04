using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkyBookMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {

            Category category = new Category();
            if (id == null) {

                return View(category);
            }

            // this is for edit request
            category = _unitOfWork.Category.Get(id.GetValueOrDefault());
            if (category == null) {
                return NotFound();
                    }
            return View(category);
        }

        #region API CALLS

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult upsert(Category category) {
            if (ModelState.IsValid) {
                if (category.Id == 0)
                {
                    _unitOfWork.Category.Add(category);
                  
                }
                else {
                    _unitOfWork.Category.Updae(category);
                        
                    }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        [HttpGet]

        public IActionResult GetAll() {

            var allObj = _unitOfWork.Category.GetAll();

            return Json(new { data = allObj });
        }

        [HttpDelete]

        public IActionResult Delete(int id) {
            var objFromDb = _unitOfWork.Category.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error While Deleting" });
            }
                _unitOfWork.Category.Remove(objFromDb);
            _unitOfWork.Save();
                return Json(new { success = true, message = "Deleted Successfully!" });
            
        }


        #endregion
    }
}

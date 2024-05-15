using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GreenGardens.DataAccess;
using GreenGardens.Models;
using GardensGardens.AccessData;
using GreenGardens.DataAccess.Repository.IRepository;

namespace Gardens.Controllers
{
    //Category Controller allows to view the Category page
    //depency injection is used to inject the ICategoryRepository interface
    /// dependency injection is a technique in which an object receives other objects that it depends on
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;
        public CategoryController(ICategoryRepository db)
        {
            _categoryRepo = db;
        }
        public IActionResult Index()
        {
            //retrieve the list of categories from the database
            //.ToList() method is used to convert the data into a list
            List<Category> objCatergoryList = _categoryRepo.GetAll().ToList();
            //return the list of categories to the view
            return View(objCatergoryList);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Name and Display Order cannot be same");
            }


            if (ModelState.IsValid)
            {
                _categoryRepo.Add(obj);
                _categoryRepo.Save();
                TempData["success"] = "Category Added Successfully";
                return RedirectToAction("Index");
            }
            return View();

        }
        //divoded








        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _categoryRepo.Get(u => u.Id == id);
            //Category? CategoryFromDB1 = _db.Categories.FirstOrDefault(u => u.Id == id);
            //Category? CategoryFromDB2 = _db.Categories.Find(id);

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
                _categoryRepo.Update(obj);
                _categoryRepo.Save();
                TempData["success"] = "Category Edited Successfully";
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
            //find the category from the database based on the id can be used to find one category
            Category? categoryFromDb = _categoryRepo.Get(u => u.Id == id);
            //Category? CategoryFromDB1 = _db.Categories.FirstOrDefault(u => u.Id == id);
            //Category? CategoryFromDB2 = _db.Categories.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }



        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _categoryRepo.Get(u => u.Id == id);
            if (obj == null)
            {
                   return NotFound();
            }

           
                _categoryRepo.Remove(obj);
                _categoryRepo.Save();
                TempData["success"] = "Category Deleted Successfully";
                return RedirectToAction("Index");
            
            

        }
    
    }
}

    




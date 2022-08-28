using BuklyBook.DataAccess;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BuklyBookWeb.Controllers
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
            IEnumerable<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        // Get
        public IActionResult Create()
        {
            return View();
        }




        // Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index"); // add controller as 2nd parameter if action is in different controller

            }
            return View(obj);
        }

        // Get
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id); // how to get the first matching record, does not throw an error for duplicates
           //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id); // how to get the first matching record, throws error if more than 1 record exists

            if(categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }


        // Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index"); // add controller as 2nd parameter if action is in different controller

            }
            return View(obj);
        }


        // Get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id); // how to get the first matching record, does not throw an error for duplicates
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id); // how to get the first matching record, throws error if more than 1 record exists

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }


        // Post
        //[HttpPost, ActionName("Delete")] // can explicitly name this action
        [HttpPost]

        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
           var obj = _db.Categories.Find(id);
            if(obj == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index"); // add controller as 2nd parameter if action is in different controller
        }
    }
}

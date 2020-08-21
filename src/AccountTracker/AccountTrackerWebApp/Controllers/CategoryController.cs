using AccountTrackerLibrary.Models;
using AccountTrackerWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccountTrackerWebApp.Controllers
{
    /// <summary>
    /// Controller for the "Category" section of the website.
    /// </summary>
    public class CategoryController : BaseController
    {
        public ActionResult Index()
        {
            IList<Category> categories = new List<Category>();
            categories = _categoryRepository.GetList();
            return View(categories);
        }

        public ActionResult Add()
        {
            Category category = new Category();
            return View(category);
        }
        
        [HttpPost]
        public ActionResult Add(Category category)
        {
            //Don't allow users to add a default category. 
            category.IsDefault = false;

            if (category.Name != null)
            {            
                ValidateCategory(category);
                
                if (ModelState.IsValid)
                {
                    //Add the category to the DB
                    _categoryRepository.Add(category);

                    TempData["Message"] = "Category successfully added.";

                    return RedirectToAction("Index");
                }
            }

            return View(category);
        }

        private void ValidateCategory(Category category)
        {
            if (_categoryRepository.NameExists(category))
            {
                ModelState.AddModelError("category.Name", "The provided category name already exists.");
            }
        }





        //Don't allow users to edit a default category. 





        //Don't allow users to delete a default category.
    }
}
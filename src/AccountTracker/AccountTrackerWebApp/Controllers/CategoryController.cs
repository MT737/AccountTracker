using AccountTrackerLibrary.Data;
using AccountTrackerLibrary.Models;
using AccountTrackerWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace AccountTrackerWebApp.Controllers
{
    /// <summary>
    /// Controller for the "Category" section of the website.
    /// </summary>
    public class CategoryController : Controller
    {
        private CategoryRepository _categoryRepository = null;

        public CategoryController(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public ActionResult Index()
        {   
            IList<Category> categories = new List<Category>();
            categories = _categoryRepository.GetList();
            return View(categories);
        }

        public ActionResult Add()
        {
            ViewModel vm = new ViewModel();
            vm.CategoryOfInterest = new Category();
            
            return View(vm);
        }
        
        [HttpPost]
        public ActionResult Add(ViewModel vm)
        {
            //Don't allow users to add a default category. 
            vm.CategoryOfInterest.IsDefault = false;

            if (vm.CategoryOfInterest.Name != null)
            {            
                ValidateCategory(vm.CategoryOfInterest);
                
                if (ModelState.IsValid)
                {
                    //Add the category to the DB
                    _categoryRepository.Add(vm.CategoryOfInterest);

                    TempData["Message"] = "Category successfully added.";

                    return RedirectToAction("Index");
                }
            }

            return View(vm);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Don't allow users to edit a default category. Should be prevented by the UI, but confirming here. 
            Category category = _categoryRepository.Get((int)id, false);
            if (!category.IsDefault)
            {
                ViewModel vm = new ViewModel();
                vm.CategoryOfInterest= category;

                return View(vm);
            }

            TempData["Message"] = "Adjustment of default categories is not allowed.";

            return RedirectToAction("Index");
        }

        //TODO: Don't need VM for this, as only a category object is required. Consider simplifying this and the ViewModel.
        [HttpPost]
        public ActionResult Edit(ViewModel vm)
        {
            if (vm.CategoryOfInterest.Name != null)
            {
                //Validate the category
                ValidateCategory(vm.CategoryOfInterest);

                if (ModelState.IsValid)
                {
                    //Update the category in the DB
                    _categoryRepository.Update(vm.CategoryOfInterest);

                    TempData["Message"] = "Category successfully updated.";

                    return RedirectToAction("Index");
                }
            }

            return View(vm);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Don't allow users to delete a default category.
            Category category = new Category();
            category = _categoryRepository.Get((int)id, false);

            if (!category.IsDefault)
            {
                ViewModel vm = new ViewModel();
                vm.CategoryOfInterest = category;
                vm.AbsorptionCategory = new Category();
                vm.CategorySelectList = vm.InitCategorySelectList(_categoryRepository);

                return View(vm);
            }

            TempData["Message"] = "Deleting default categories is not allowed.";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(ViewModel vm)
        {
            bool errorMessageSet = false;            

            //Check for absorption category selection.
            if (vm.AbsorptionCategory.CategoryID != 0)
            {
                Category absorbedCategory = _categoryRepository.Get(vm.CategoryOfInterest.CategoryID, false);
                Category absorbingCategory = _categoryRepository.Get(vm.AbsorptionCategory.CategoryID, false);

                //Ensure that the deleted category is not default.
                if (!absorbedCategory.IsDefault)
                {
                    //Make sure the absorbing category and the deleting category are not the same.
                    if (absorbedCategory.CategoryID != absorbingCategory.CategoryID)
                    {
                        //Update all transactions that currently point to the category being deleted to instead point to the absorbing category.
                        _categoryRepository.Absorption(absorbedCategory.CategoryID, absorbingCategory.CategoryID);

                        //Delete the category to be deleted.
                        _categoryRepository.Delete(absorbedCategory.CategoryID);

                        TempData["Message"] = "Category successfully deleted.";

                        return RedirectToAction("Index");
                    }
                    SetErrorMessage(vm, "Category being deleted and category absorbing cannot be the same.", errorMessageSet);
                    errorMessageSet = true;
                }
                SetErrorMessage(vm, "Deleting a default category is not allowed.", errorMessageSet);
                errorMessageSet = true;
            }
            SetErrorMessage(vm, "You must select a category to absorb transactions related to the category being deleted.", errorMessageSet);

            ViewModel failureStateVM = new ViewModel();
            failureStateVM.CategoryOfInterest = _categoryRepository.Get(vm.CategoryOfInterest.CategoryID, false);            
            failureStateVM.CategorySelectList = failureStateVM.InitCategorySelectList(_categoryRepository);
            return View(failureStateVM);
        }

        private void SetErrorMessage(ViewModel vm, string message, bool messageSet)
        {
            //If there's already an error message, don't do anything.
            if (!messageSet)
            {
                foreach (var modelValue in ModelState.Values)
                {
                    modelValue.Errors.Clear();
                }

                ModelState.AddModelError("Category", message);
            }
        }

        private void ValidateCategory(Category category)
        {
            if (_categoryRepository.NameExists(category))
            {
                ModelState.AddModelError("category.Name", "The provided category name already exists.");
            }
        }
    }
}
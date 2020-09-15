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
    /// Controller for the "Vendor" section of the website.
    /// </summary>
    public class VendorController : Controller
    {
        private VendorRepository _vendorRepository = null;

        public VendorController(VendorRepository vendorRepository)
        {
            _vendorRepository = vendorRepository;
        }

        public ActionResult Index()
        {
            IList<Vendor> vendors = new List<Vendor>();
            vendors = _vendorRepository.GetList();

            return View(vendors);
        }

        public ActionResult Add()
        {
            Vendor vendor = new Vendor();
            return View(vendor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Vendor vendor)
        {
            //Don't allow users to add a default vendor
            vendor.IsDefault = false;

            //TODO: Test trying to add a vendor by navigating directly to the page without input.
            if (vendor.Name != null)
            {
                ValidateVendor(vendor);

                if (ModelState.IsValid)
                {
                    //Add vendor to the DB
                    _vendorRepository.Add(vendor);

                    TempData["Message"] = "Vendor successfully added.";

                    return RedirectToAction("Index");
                }
            }

            return View(vendor);
        }
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Don' allow users to edit default vendors.
            Vendor vendor = _vendorRepository.Get((int)id, false);

            if (!vendor.IsDefault)
            {
                return View(vendor);
            }

            TempData["Message"] = "Adjustment of default vendor is not allow.";

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Vendor vendor)
        {
            //Make sure to validate and don't allow edit of default vendors
            if (vendor.Name != null)
            {
                //Validate vendor
                ValidateVendor(vendor);

                if (ModelState.IsValid)
                {
                    //Update the Vendor in the DB
                    _vendorRepository.Update(vendor);

                    TempData["Message"] = "Vendor successfully updated.";

                    return RedirectToAction("Index");
                }
            }

            return View(vendor);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Don't allow users to delete a default vendor
            Vendor vendorToDelete = _vendorRepository.Get((int)id, false);
            if (!vendorToDelete.IsDefault)
            {   
                //Instantiate a vm to hold vendor to delete, vendor select list, and vendor to absorb.
                ViewModel vm = new ViewModel();
                vm.VendorOfInterest = vendorToDelete;
                vm.AbsorptionVendor = new Vendor();
                vm.VendorSelectList = vm.InitVendorSelectList(_vendorRepository);

                return View(vm);
            }

            TempData["Message"] = "Deleting default vendor is not allowed.";

            return RedirectToAction("Index");
        }
            
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ViewModel vm)
        {
            bool errorMessageSet = false;

            //Check for absorption vendor selection.
            if (vm.AbsorptionVendor.VendorID != 0)
            {
                Vendor absorbedVendor = _vendorRepository.Get(vm.VendorOfInterest.VendorID, false);
                Vendor absorbingVendor = _vendorRepository.Get(vm.AbsorptionVendor.VendorID, false);

                //Ensure that the deleted vendor is not default.
                if (!absorbedVendor.IsDefault)
                {
                    //Make sure the absorbing vendor and the deleting vendor are not the same.
                    if (absorbedVendor.VendorID != absorbingVendor.VendorID)
                    {
                        //Update all transactions that currently point to the vendor being deleted to instead point to the absorbing vendor.
                        _vendorRepository.Absorption(absorbedVendor.VendorID, absorbingVendor.VendorID);

                        //Delete the vendor to be deleted.
                        _vendorRepository.Delete(absorbedVendor.VendorID);

                        TempData["Message"] = "Vendor successfully deleted.";

                        return RedirectToAction("Index");
                    }
                    SetErrorMessage(vm, "Vendor being deleted and vendor absorbing cannot be the same.", errorMessageSet);
                    errorMessageSet = true;
                }
                SetErrorMessage(vm, "Deleting a default vendor is not allowed.", errorMessageSet);
                errorMessageSet = true;
            }
            SetErrorMessage(vm, "You must select a vendor to absorb transactions related to the vendor being deleted.", errorMessageSet);

            ViewModel failureStateVM = new ViewModel();
            failureStateVM.VendorOfInterest = _vendorRepository.Get(vm.VendorOfInterest.VendorID, false);
            failureStateVM.VendorSelectList = failureStateVM.InitVendorSelectList(_vendorRepository);
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

                ModelState.AddModelError("Vendor", message);
            }
        }

        private void ValidateVendor(Vendor vendor)
        {
            if (_vendorRepository.NameExists(vendor))
            {
                ModelState.AddModelError("Name", "The provided vendor name already exists.");
            }
        }
        
    }
}
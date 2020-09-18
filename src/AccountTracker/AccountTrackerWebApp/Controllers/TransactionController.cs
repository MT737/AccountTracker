using AccountTrackerLibrary.Models;
using System.Net;
using AccountTrackerLibrary.Data;
using AccountTrackerWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace AccountTrackerWebApp.Controllers
{
    /// <summary>
    /// Controller for the "Transction" Portion of the website.
    /// </summary>
    public class TransactionController : Controller
    {
        private AccountRepository _accountRepository = null;
        private CategoryRepository _categoryRepository = null;
        private TransactionRepository _transactionRepository = null;
        private TransactionTypeRepository _transactionTypeRepository = null;
        private VendorRepository _vendorRepository = null;
        
        public TransactionController(AccountRepository accountRepository, CategoryRepository categoryRepository, TransactionRepository transactionRepository,
            TransactionTypeRepository transactionTypeRepository, VendorRepository vendorRepository)
        {
            _accountRepository = accountRepository;
            _categoryRepository = categoryRepository;
            _transactionRepository = transactionRepository;
            _transactionTypeRepository = transactionTypeRepository;
            _vendorRepository = vendorRepository;
        }

        public ActionResult Index()
        {
            //Instantiate viewmodel
            var vm = new ViewModel();
            var userID = User.Identity.GetUserId();

            //Complete viewmodel property required for transaction view
            vm.Transactions = GetTransactionsWithDetails(userID);

            return View(vm);
        }

        public ActionResult Add()
        {
            //Instantiate viewmodel
            var vm = new ViewModel();

            //Instantiate Transaction of Interest property
            vm.TransactionOfInterest = new Transaction();

            //Preset default values
            vm.TransactionOfInterest.TransactionDate = DateTime.Now.Date;
            vm.TransactionOfInterest.Amount = 0.00M;
            vm.TransactionOfInterest.UserID = User.Identity.GetUserId();

            //TODO: Consider limiting the select list items. Probably shouldn't allow users to select new account balance.
            //Initialize set list items.
            vm.Init(vm.TransactionOfInterest.UserID, _transactionTypeRepository, _accountRepository, _categoryRepository, _vendorRepository);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ViewModel vm)
        {
            //Don't trust the passed userID. 
            vm.TransactionOfInterest.UserID = User.Identity.GetUserId(); 
            
            //TODO: Confirm validation requirements.
            if (ModelState.IsValid)
            {
                 _transactionRepository.Add(vm.TransactionOfInterest);

                TempData["Message"] = "Transaction successfully added.";

                return RedirectToAction("Index");
            }

            vm.Init(vm.TransactionOfInterest.UserID, _transactionTypeRepository, _accountRepository, _categoryRepository, _vendorRepository);
            return View(vm);
        }

        public ActionResult Edit(int? id)
        {
            //Confirm id is not null
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Get transaction if it exists.
            var userID = User.Identity.GetUserId();
            Transaction transaction = _transactionRepository.Get((int)id, userID, true);
            
            //Confirm transaction exists. This doubles as ensuring that the user owns the transaction, as the object will be null if the UserID and TransactionID combo don't exist.            
            if (transaction == null)
            {
                return HttpNotFound();
            }

            //Instantiate viewmodel and set transactionofinterest property
            var vm = new ViewModel { TransactionOfInterest = transaction };
            
            //Initialize select list items
            vm.Init(vm.TransactionOfInterest.UserID, _transactionTypeRepository, _accountRepository, _categoryRepository, _vendorRepository);
           
            //Return the view
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ViewModel vm)
        {
            //TODO: Currently allowing edit even when nothing is changed. Fix that.
            //TODO: Confirm additional validation requirements.
            
            vm.TransactionOfInterest.UserID = User.Identity.GetUserId();

            //Confirm user owns the transaction
            if (!_transactionRepository.UserOwnsTransaction(vm.TransactionOfInterest.TransactionID, vm.TransactionOfInterest.UserID))
            {
                return HttpNotFound();
            }

            //If model state is valid, update the db and redirect to index.
            if (ModelState.IsValid)
            {
                //Don't trust client passed userID
                _transactionRepository.Update(vm.TransactionOfInterest);
                TempData["Message"] = "Your transaction was updated successfully.";

                return RedirectToAction("Index");
            }

            //If model state is in error, reinit the select lists and call the edit view again.
            vm.Init(vm.TransactionOfInterest.UserID, _transactionTypeRepository, _accountRepository, _categoryRepository, _vendorRepository);
            return View(vm);
        }

        public ActionResult Delete(int? id)
        {
            //Confirm id is not null
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Get transaction if it exists.
            var userID = User.Identity.GetUserId();
            Transaction transaction = _transactionRepository.Get((int)id, userID, true);

            //Confirm transaction exists. This doubles as ensuring that the user owns the transaction, as the object will be null if the UserID and TransactionID combo don't exist.
            if (transaction == null)
            {
                return HttpNotFound();
            }

            //Instantiate viewmodel and set transactionofinterest property
            var vm = new ViewModel { TransactionOfInterest = transaction };

            //Return the view
            return View(vm);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var userID = User.Identity.GetUserId();

            //Make sure the user owns the transaction before deleting it.
            if (!_transactionRepository.UserOwnsTransaction(id, userID))
            {
                return HttpNotFound();
            }

            _transactionRepository.Delete(id);
            TempData["Message"] = "Transaction successfully deleted.";
            return RedirectToAction("Index");
        }

        //TODO: This exact method is used in the Dashboard controller as well. Put in a central location (DRY).
        /// <summary>
        /// Retrieves a list of transactions associated to the specified user.
        /// </summary>
        /// <param name="userID">String: UserID for which to pull a list of transactions.</param>
        /// <returns>IList of Transaction objects.</returns>
        public IList<Transaction> GetTransactionsWithDetails(string userID)
        {
            //Get get a list of transactions to gain access to transaction ids
            IList<Transaction> transactions = new List<Transaction>();
            foreach (var transaction in _transactionRepository.GetList(userID))
            {
                //Get the detailed data for each transaction and add it to the IList of transactions
                transactions.Add(_transactionRepository.Get(transaction.TransactionID, userID, true));
            }

            return transactions;
        }
    }
}
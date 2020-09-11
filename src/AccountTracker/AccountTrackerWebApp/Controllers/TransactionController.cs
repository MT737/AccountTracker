using AccountTrackerLibrary.Models;
using System.Net;
using AccountTrackerLibrary.Data;
using AccountTrackerWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        //Index page
        public ActionResult Index()
        {
            //Instantiate viewmodel
            var vm = new ViewModel();

            //Complete viewmodel property required for transaction view
            vm.Transactions = GetTransactionsWithDetails();

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

            //TODO: Consider limiting the select list items. Probably shouldn't allow users to select new account balance.
            //Initialize set list items.
            vm.Init(_transactionTypeRepository, _accountRepository, _categoryRepository, _vendorRepository);

            return View(vm);
        }

        [HttpPost]
        public ActionResult Add(ViewModel vm)
        {
            //TODO: Confirm validation requirements.

            if (ModelState.IsValid)
            {
                Transaction transaction = new Transaction();
                transaction = vm.TransactionOfInterest;
                _transactionRepository.Add(transaction);

                TempData["Message"] = "Transaction successfully added.";

                return RedirectToAction("Index");
            }

            vm.Init(_transactionTypeRepository, _accountRepository, _categoryRepository, _vendorRepository);
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
            Transaction transaction = _transactionRepository.Get((int)id, true);
            
            //Confirm transaction exists.
            if (transaction == null)
            {
                return HttpNotFound();
            }

            //Instantiate viewmodel and set transactionofinterest property
            var vm = new ViewModel { TransactionOfInterest = transaction };
            
            //Initialize select list items
            vm.Init(_transactionTypeRepository, _accountRepository, _categoryRepository, _vendorRepository);
           
            //Return the view
            return View(vm);
        }

        [HttpPost]
        public ActionResult Edit(ViewModel vm)
        {
            //TODO: Currently allowing edit even when nothing is changed. Fix that.
            //TODO: Confirm additional validation requirements.

            //If model state is valid, update the db and redirect to index.
            if (ModelState.IsValid)
            {
                Transaction transaction = vm.TransactionOfInterest;

                _transactionRepository.Update(transaction);

                TempData["Message"] = "Your transaction was updated successfully.";

                return RedirectToAction("Index");
            }

            //If model state is in error, reinit the select lists and call the edit view again.
            vm.Init(_transactionTypeRepository, _accountRepository, _categoryRepository, _vendorRepository);
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
            Transaction transaction = _transactionRepository.Get((int)id, true);

            //Confirm transaction exists.
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
        public ActionResult Delete(int id)
        {
            _transactionRepository.Delete(id);
            TempData["Message"] = "Transaction successfully deleted.";
            return RedirectToAction("Index");
        }

        //TODO: This exact method is used in the Dashboard controller as well. Put in a central location (DRY).
        public IList<Transaction> GetTransactionsWithDetails()
        {
            //Get get a list of transactions to gain access to transaction ids
            IList<Transaction> transactions = new List<Transaction>();
            foreach (var transaction in _transactionRepository.GetList())
            {
                //Get the detailed data for each transaction and add it to the IList of transactions
                transactions.Add(_transactionRepository.Get(transaction.TransactionID, true));
            }

            return transactions;
        }
    }
}
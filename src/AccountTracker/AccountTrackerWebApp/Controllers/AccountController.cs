﻿using AccountTrackerLibrary.Models;
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
    /// Controller for the "Account" section of the website.
    /// </summary>
    public class AccountController : BaseController
    {
        public ActionResult Index()
        {
            IList<ViewModel.AccountWithBalance> accounts = new List<ViewModel.AccountWithBalance>();
            accounts = GetAccountWithBalances();

            return View(accounts);
        }

        public ActionResult Add()
        {
            //Instantiate an empty ViewModel
            ViewModel vm = new ViewModel();

            //Instantiate an empty AccountOfInterest property of the VM
            vm.AccountOfInterest = new Account();
            vm.TransactionOfInterest = new Transaction();

            //Call the view and pass the VM.
            return View(vm);
        }

        [HttpPost]
        public ActionResult Add(ViewModel vm)
        {
            if (vm.AccountOfInterest.Name != null)
            {
                //Validate the new account
                ValidateAccount(vm.AccountOfInterest);

                //Confirm valid modelstate
                if (ModelState.IsValid)
                {
                    //Add the account to the account table in the DB.
                    Account account = new Account();
                    account = vm.AccountOfInterest;
                    _accountRepository.Add(account);

                    //Add a transaction to the transaction table in the DB to create the initial account balance.                
                    CompleteAccountTransaction(vm, true);
                    _transactionRepository.Add(vm.TransactionOfInterest);

                    TempData["Message"] = "Account successfully added.";

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

            ViewModel vm = new ViewModel();
            vm.TransactionOfInterest = new Transaction();
            vm.AccountOfInterest = _accountRepository.Get((int)id);            
            vm.TransactionOfInterest.Amount = _accountRepository.GetBalance((int)id, vm.AccountOfInterest.IsAsset);

            return View(vm);
        }

        [HttpPost]
        public ActionResult Edit(ViewModel vm)
        {
            if (vm.AccountOfInterest.Name != null)
            {
                //Validate the account
                ValidateAccount(vm.AccountOfInterest);

                if (ModelState.IsValid)
                {
                    //Grab current balance before updating the DB
                    decimal currentBalance = _accountRepository.GetBalance(vm.AccountOfInterest.AccountID, vm.AccountOfInterest.IsAsset);

                    //Update the account table in the DB
                    Account account = new Account();
                    account = vm.AccountOfInterest;
                    _accountRepository.Update(account);

                    //If balance changed, add an adjustment transaction.
                    if (currentBalance != vm.TransactionOfInterest.Amount)
                    {
                        //Determine adjustment
                        vm.TransactionOfInterest.Amount = vm.TransactionOfInterest.Amount - currentBalance;

                        //Add a transaction to the transaction table in the DB to create an account balance adjustment transaction.                
                        CompleteAccountTransaction(vm, false);
                        _transactionRepository.Add(vm.TransactionOfInterest);
                        
                    }

                    TempData["Message"] = "Account successfully edited.";

                    return RedirectToAction("Index");
                }
            }

            return View(vm);
        }

        //Leaving the schafolding here, but not going to currently allow users to delete an account in order to maintain transaction data integrity.
        //public ActionResult Delete(int? id)
        //{
        //    return HttpNotFound();
        //}

        //[HttpPost]
        //public ActionResult Delete(int id)
        //{
        //    return HttpNotFound();
        //}

        private void ValidateAccount(Account accountOfInterest)
        {
            //New or updated Accounts cannot have the same name as currently existing accounts (except for the same account if editing).
            if (_accountRepository.NameExists(accountOfInterest.Name, accountOfInterest.AccountID))
            {
                ModelState.AddModelError("accountOfInterest.Name", "The provided account name already exsits.");
            }
        }

        private void CompleteAccountTransaction(ViewModel vm, bool newAccount)
        {
            vm.TransactionOfInterest.AccountID = _accountRepository.GetID(vm.AccountOfInterest.Name);
            vm.TransactionOfInterest.VendorID = _vendorRepository.GetID("N/A");
            vm.TransactionOfInterest.TransactionDate = DateTime.Now.Date;
            
            if (newAccount)
            {
                vm.TransactionOfInterest.CategoryID = _categoryRepository.GetID("New Account");
                vm.TransactionOfInterest.Description = "New Account";
            }
            else
            {
                vm.TransactionOfInterest.CategoryID = _categoryRepository.GetID("Account Correction");
                vm.TransactionOfInterest.Description = "Account Balance Adjustment";
            }

            if (vm.AccountOfInterest.IsAsset)
            {
                vm.TransactionOfInterest.TransactionTypeID = _transactionTypeRepository.GetID("Payment To");
            }
            else
            {
                vm.TransactionOfInterest.TransactionTypeID = _transactionTypeRepository.GetID("Payment From");
            }    
        }
    }
}
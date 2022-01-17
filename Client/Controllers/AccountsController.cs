using API.Models;
using API.ViewModel;
using Client.Base.Controllers;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository accountRepository;
        public AccountsController(AccountRepository repository) : base(repository)
        {
            accountRepository = repository;
        }
        public IActionResult Index()
        {
            return View("Accounts");
        }

        [HttpPost("Accounts/Login")]
        public async Task<IActionResult> Auth(LoginVM login)
        {
            var jwtToken = await accountRepository.Auth(login);
            var token = jwtToken.idToken;

            if (token == null)
            {
                return RedirectToAction("Index", "Accounts");
            }

            HttpContext.Session.SetString("JWToken", token);


            return RedirectToAction("Index", "Employees");
        }

        [HttpGet("Accounts/Logout")]
        public IActionResult Logout()
        {


            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Accounts");
        }
    }
}

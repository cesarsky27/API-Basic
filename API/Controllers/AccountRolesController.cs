using API.Base;
using API.Context;
using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountRolesController : BaseController<AccountRole, AccountRoleRepository, string>
    {
        private AccountRoleRepository accountRoleRepository;
        private readonly MyContext myContext;
        public IConfiguration _configuration;

        public AccountRolesController (AccountRoleRepository accountRoleRepository, MyContext myContext, IConfiguration configuration) : base (accountRoleRepository)
        {
            this.accountRoleRepository = accountRoleRepository;
            this.myContext = myContext;
            this._configuration = configuration;
        }

        [Authorize(Roles = "Director, Manager")]
        [HttpPost("SignManager")]
        public ActionResult Post (AccountRoleVM accountRoleVM)
        {
            try
            {
                var result = accountRoleRepository.SignManager(accountRoleVM);
                if (result != 1)
                {
                    return BadRequest(new { status = StatusCodes.Status204NoContent, message = "Data yang anda masukan sama!" });
                }
                else
                {
                    return Ok(new { status = StatusCodes.Status200OK, message = "Data berhasil dibuat!" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { status = StatusCodes.Status404NotFound, message = "Gagal memasukkan data!" });
            }
        }

    }
}

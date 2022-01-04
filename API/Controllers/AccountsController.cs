using API.Base;
using API.Context;
using API.Models;
using API.ViewModel;
using API.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, int>
    {
        private AccountRepository accountRepository;
        public IConfiguration _configuration;
        private readonly MyContext myContext;
        public AccountsController(MyContext myContext, AccountRepository accountRepository, IConfiguration configuration) : base (accountRepository)
        {
            this.accountRepository = accountRepository;
            this._configuration = configuration;
            this.myContext = myContext;
        }

        //1.1 FIX
        [HttpPost("Login")]
        public ActionResult Post(LoginVM loginVM)
        {
            var result = accountRepository.Login(loginVM);
            if (result == "2")
            {
                return NotFound(new { status = StatusCodes.Status400BadRequest, result, message = "Password Salah" });
            }
            else if (result == "0")
            {
                return NotFound(new { status = StatusCodes.Status404NotFound, result, message = "Login Gagal" });
            }
            else if (result == "3")
            {
                return NotFound(new { status = StatusCodes.Status204NoContent, result, message = "Email yang anda masukan salah atau tidak terdaftar!" });
            }
            else
            {
                var profile = accountRepository.GetProfile(result);
                if (profile != null)
                {
                    var getUserData = (from e in myContext.Employees
                                       where e.Email == loginVM.Email
                                       join a in myContext.Set<Account>() on e.NIK equals a.NIK
                                       join ar in myContext.Set<AccountRole>() on e.NIK equals ar.AccountNIK
                                       join r in myContext.Set<Role>() on ar.RoleId equals r.RoleID
                                       select new
                                       {
                                           name = r.RoleName,
                                           email = e.Email
                                       }).ToList();
                    var claims = new List<Claim>
                    {
                        new Claim("Email", getUserData[0].email),
                        //new Claim("Roles", "Manager")
                        //new Claim("Roles", getUserData[0].name)
                    };
                    foreach (var useRole in getUserData)
                    {
                        claims.Add(new Claim("Roles: ", useRole.name));
                    }
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims,
                        expires: DateTime.UtcNow.AddMinutes(10), signingCredentials: signIn);
                    var idToken = new JwtSecurityTokenHandler().WriteToken(token);
                    claims.Add(new Claim("Token Security", idToken.ToString()));
                    return Ok(new { status = StatusCodes.Status200OK, idToken, message = "Success Login" });

                }
            }
            //else
            //{
            //    return BadRequest(new { status = StatusCodes.Status404NotFound, result, message = "Login Failed" });
            //}
            return Ok(new { status = StatusCodes.Status200OK, result, message = "Login Berhasil" });
        }
        [HttpPost("ForgotPass")]
        public ActionResult ForgotPass(ForgotPasswordVM forgotPasswordVM)
        {
            var result = accountRepository.ForgotPassword(forgotPasswordVM);
            if (result == 2)
            {
                return NotFound(new { status = StatusCodes.Status404NotFound, result, message = "" });
            }
            else if (result == 3)
            {
                return NotFound(new { status = StatusCodes.Status204NoContent, result, message = "" });
            }
            else if (result == 1)
            {
                return Ok(new { status = StatusCodes.Status200OK, result, message = "Kode OTP telah dikirimkan, harap cek E-Mail anda!" });
            }
            return NotFound(new { status = StatusCodes.Status500InternalServerError, result, message = "Data Tidak Ditemukan" });
        }
        [Authorize]
        [Route("TestJWT")]
        [HttpGet]
        public ActionResult TestJWT()
        {
            return Ok("Test JWT Berhasil");
        }

        [HttpPost("ChangePass")]
        public ActionResult ChangePass(ChangePasswordVM changepassVM)
        {
            var result = accountRepository.ChangePassword(changepassVM);
            if (result == 2)
            {
                return NotFound(new { status = StatusCodes.Status404NotFound, result, message = "Kode OTP telah digunakan, harap Generate kode lain!" });
            }
            else if (result == 3)
            {
                return NotFound(new { status = StatusCodes.Status204NoContent, result, message = "Konfirmasi password tidak sesuai dengan new password" });
            }
            else if (result == 1)
            {
                return Ok(new { status = StatusCodes.Status200OK, result, message = "Data Password kamu telah diperbarui" });
            }
            else if (result == 4)
            {
                return Ok(new { status = StatusCodes.Status400BadRequest, result, message = "Kode OTP yang anda masukkan salah!" });
            }
            else if (result == 6)
            {
                return NotFound(new { status = StatusCodes.Status403Forbidden, result, message = "Kode OTP telah expired" });
            }
            return NotFound(new { status = StatusCodes.Status403Forbidden, result, message = "Data tidak ditemukan" });
        }
    }
}



        //COBA-2 BISA
        //[HttpPost("Login")]
        //public ActionResult Login(LoginVM loginVM)
        //{
        //    var code = 0;
        //    var message = "";
        //    var result = accountRepository.Login(loginVM);

        //    switch (result)
        //    {
        //        case 1:
        //            {
        //                code = StatusCodes.Status200OK;
        //                message = $"Login Berhasil";
        //                break;
        //            }
        //        case 2:
        //            {
        //                code = StatusCodes.Status400BadRequest;
        //                message = $"Password Salah";
        //                break;
        //            }
        //        case 3:
        //            {
        //                code = StatusCodes.Status404NotFound;
        //                message = $"Email Not Found";
        //                break;
        //            }
        //        default:
        //            {
        //                code = StatusCodes.Status400BadRequest;
        //                message = $"Login Gagal";
        //                break;
        //            }
        //    }
        //    return Ok(new { code, result, message });
        //}
using API.Base;
using API.Context;
using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private AccountRepository accountRepository;
        public IConfiguration _configuration;
        private readonly MyContext myContext;
        public AccountsController(AccountRepository repository, IConfiguration configuration, MyContext myContext) : base(repository)
        {
            this.accountRepository = repository;
            this._configuration = configuration;
            this.myContext = myContext;
        }

        [HttpPost("Login")]
        public ActionResult Post(LoginVM loginVM)
        {
            var result = accountRepository.Login(loginVM);
            if (result == 1)
            {
                /* return Ok(new { status = HttpStatusCode.OK, result, messageResult = "berhasil login" });/
                return RedirectToAction("GetAll",loginVM);
                /     return Redirect(“Accounts / All”);*/
                var getUserData = (
                                    from e in myContext.Set<Employee>()
                                    join a in  myContext.Set<Account>() on e.NIK equals a.NIK
                                    join ar in myContext.Set<AccountRole>() on a.NIK equals ar.AccountNIK
                                    join r in myContext.Set<Role>() on ar.RoleId equals r.RoleId
                                    where e.Email == loginVM.Email 
                                    select new
                                    {
                                        Email = e.Email,
                                        Name = r.Name
                                    }).ToList();
            
                var claims = new List<Claim>
                        {
                            new Claim(JwtRegisteredClaimNames.Email, getUserData[0].Email)
                        };

                foreach (var userRole in getUserData)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole.Name));
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: signIn
                    );

                var idtoken = new JwtSecurityTokenHandler().WriteToken(token);
                claims.Add(new Claim("TokenSecurity", idtoken.ToString()));

             /*   return Ok(result);*/
                return Ok(new JWTokenVM { Token = idtoken, Messages = "Login Sucsses" });
                /* return Ok(new { status = HttpStatusCode.OK, token = idtoken, data = accountRepository.GetProfile(loginVM),Message = $"Login Berhasil" });*/
                /*             return Ok(new
                             {
                                 status = HttpStatusCode.OK,
                                 data = accountRepository.GetProfile(loginVM),
                                 message = "Berhasil Login"
                             });*/

            }
            else if (result == 2)
            {
                return NotFound(new { status = HttpStatusCode.NoContent, result, messageResult = "Password Salah" });
            }
            else if (result == 3)
            {
                return NotFound(new { status = HttpStatusCode.NoContent, result, messageResult = "Akun tidak ditemukan" });
            }

            return NotFound(new { status = HttpStatusCode.BadRequest, result, messageResult = "gagal login" });
        }

        [HttpPut("ChangePassword")]
        public ActionResult ChangePassword(ChangePasswordVM changePasswordVM)
        {
            var result = accountRepository.ChangePassword(changePasswordVM);
            if (result == 1)
            {
                return Ok(new { status = HttpStatusCode.OK, result = result, Message = "Password berhasil diubah" });
            }
            else if (result == 2)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, Message = "Konfirmasi Pasword salah" });
            }
            else if (result == 3)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, Message = "Password anda salah" });
            }

            else if (result == 4)
            {
                return BadRequest(new { status = HttpStatusCode.NoContent, Message = "Akun tidak ditemukan" });
            }
            return BadRequest(new { status = HttpStatusCode.BadRequest, Message = "Gagal mengubah Password" });

        }

        [HttpPost("ForgotPassword")]
        public ActionResult ForgotPassword(ForgetPasswordVM forgetPasswordVM)
        {
            var result = accountRepository.ForgotPassword(forgetPasswordVM);
            if (result == 1)
            {
                return Ok(new { status = HttpStatusCode.OK, result = result, Message = "Forgot Password Berhasil, Password Baru Anda Telah di Kirimkan Ke Email Anda" });
            }
            else if (result == 2)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, Message = "Gagal mengirim Email" });
            }
            else if (result == 3)
            {

                return BadRequest(new { status = HttpStatusCode.BadRequest, Message = "Akun tidak ditemukan" });
            }
            return BadRequest(new { status = HttpStatusCode.BadRequest, Message = "Gagal melakukan perintah forgot password" });

        }

        [Authorize]
        [HttpGet("TestJWT")]
        public ActionResult TestJWT()
        {
            return Ok("Test JWT Berhasil");
        }
    }

}



using API.Base;
using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountRolesController : BaseController<AccountRole, AccountRoleRepository, string>
    {
        private AccountRoleRepository accountroleRepository;

        public AccountRolesController(AccountRoleRepository repository) : base(repository)
        {
            this.accountroleRepository = repository;
        }
        [Authorize(Roles = "Director")]
        [HttpPost("SignManager")]
        public ActionResult Post(AccountRole accountRole)
        {
            var result = accountroleRepository.SignManager(accountRole);
            if (result == 1)
            {
                return Ok(new { status = HttpStatusCode.OK, result, messageResult = "Data berhasil ditambahkan" });
            }
            return BadRequest(new { status = HttpStatusCode.BadRequest, result, messageResult = "Sepertinya terjadi kesalahan, periksa kembali!" });
        }
    }
}

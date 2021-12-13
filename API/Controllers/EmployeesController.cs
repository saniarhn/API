using API.Base;
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
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private EmployeeRepository employeeRepository;
        public IConfiguration _configuration;

        public EmployeesController(EmployeeRepository repository, IConfiguration configuration) : base(repository)
        {
            this.employeeRepository = repository;
            this._configuration = configuration;
        }

        [HttpPost("RegisterA")]
        public ActionResult Post(RegisterVM registerVM)
        {
            var result = employeeRepository.Register(registerVM);
            if (result == 1)
            {
                /*return Ok(new { status = HttpStatusCode.OK, result, messageResult = "Data berhasil ditambahkan" });*/
                return Ok(result);

            }
            else if (result == 2)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result, messageResult = "NIK sudah digunakan" });
            }
            else if (result == 3)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result, messageResult = "Email sudah digunakan" });
            }
            else if (result == 4)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result, messageResult = "Phone sudah digunakan" });
            }
            else if (result == 5)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result, messageResult = "UniversityId tidak ditemukan" });
            }
            return BadRequest(new { status = HttpStatusCode.BadRequest, result, messageResult = "Sepertinya terjadi kesalahan, periksa kembali!" });

        }
/*        [Authorize(Roles = "Director,Manager")]*/

        [HttpGet("Register")]
        public ActionResult<Employee> GetAll()
        {
            var result = employeeRepository.GetAll();
            if (result == null)
            {
                return NotFound(new { status = HttpStatusCode.NoContent, result, messageResult = "Data masih kosong" });
            }
            //      return Ok(new { status = HttpStatusCode.OK, result, messageResult = "Semua data berhasil ditampilkan" });
            return Ok(result);
        }

       

        [HttpGet("TestCORS")]
        public ActionResult TestCORS()
        {
            return Ok("Test CORS Berhasil");
        }
    }
}

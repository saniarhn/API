using API.Models;
using API.Repository;
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
    public class OldEmployeesController : ControllerBase
    {
        private OldEmployeeRepository employeeRepository;
        public OldEmployeesController(OldEmployeeRepository employeeRepository)
        {

            this.employeeRepository = employeeRepository;

        }

        [HttpPost]
        public ActionResult Post(Employee employee)
        {
            var result = employeeRepository.Insert(employee);
            if (result != 0)
            {
                return Ok(new { status = HttpStatusCode.OK, result, messageResult = "Data berhasil ditambahkan" });
            }

            return BadRequest(new { status = HttpStatusCode.BadRequest, result, messageResult = "Sepertinya terjadi kesalahan, periksa kembali!" });
        }

        [HttpGet]
        public ActionResult Get()
        {
            var result = employeeRepository.Get();
            if (result.Count() == 0)
            {
                return NotFound(new { status = HttpStatusCode.NoContent, result, messageResult = "Data masih kosong" });
            }
            return Ok(new { status = HttpStatusCode.OK, result, messageResult = "Semua data berhasil ditampilkan" });
        }

        [HttpDelete("{NIK}")]
        public ActionResult Delete(string NIK)
        {
            var result = employeeRepository.Delete(NIK);
            if (result != 0)
            {
                return Ok(new { status = HttpStatusCode.OK, result, messageResult = $"Data {NIK} berhasil dihapus" });
            }
            return NotFound(new { status = HttpStatusCode.NotFound, result, messageResult = $"Data {NIK} tidak ditemukan." });
        }

        [HttpGet("{NIK}")]
        public ActionResult Get(string NIK)
        {
            var result = employeeRepository.Get(NIK);
            if (result != null)
            {
                return Ok(new { status = HttpStatusCode.OK, result, messageResult = "Data ditemukan" });
            }
            return NotFound(new { status = HttpStatusCode.NotFound, result, messageResult = $"Data {NIK} tidak ditemukan." });
        }

        [HttpPut]
        public ActionResult Put(Employee employee)
        {
            var result = employeeRepository.Update(employee);
            if (result != 0)
            {
                return Ok(new { status = HttpStatusCode.OK, result, messageResult = "Data Berhasil di Update" });
            }
            return BadRequest(new { status = HttpStatusCode.BadRequest, result, messageResult = "Data tidak berhasil di update" });
        }

    }
}

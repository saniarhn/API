using API.Base;
using API.Models;
using API.Repository.Data;
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
    public class UniversitiesController : BaseController<University, UniversityRepository, int>
    {
        private UniversityRepository universitiesRepository;

        public UniversitiesController(UniversityRepository repository) : base(repository)
        {
            this.universitiesRepository = repository;
        }
 
   

    
        [HttpGet("CountUniversity")]
        public ActionResult GetCountUniversity()
        {
            var result = universitiesRepository.GetCountUniversity();
            if (result.Key == null)
            {
                return NotFound(new { status = HttpStatusCode.NoContent, result, messageResult = "Data masih kosong" });
            }
            return Ok(new { status = HttpStatusCode.OK, result, messageResult = "Semua data berhasil ditampilkan" });
        }
    }
}

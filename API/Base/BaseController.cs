﻿using System;
using System.Linq;
using System.Net;
using API.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IRepository<Entity, Key>
    {
        private Repository repository;

        public BaseController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult<Entity> Get()
        {
            var result = repository.Get();
            if (result.Count() == 0)
            {
                return NotFound(new { status = HttpStatusCode.NoContent, result, messageResult = "Data masih kosong" });
            }
            /*  return Ok(new { status = HttpStatusCode.OK, result, messageResult = "Semua data berhasil ditampilkan" });*/
            return Ok(result);
        }
  
        [HttpPost]
        public ActionResult Post(Entity entity)
        {
            try
            {
                var result = repository.Insert(entity);
                /*   return Ok(new { status = HttpStatusCode.OK, result = result, Message = "Data telah berhasil di buat" });*/
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, Message = "Gagal memasukan data" });
            }
        }

        [HttpPut]
        public ActionResult<Entity> Put(Entity entity)
        {
            var result = repository.Update(entity);
            if (result != 0)
            {
                /* return Ok(new { status = HttpStatusCode.OK, result, messageResult = "Data Berhasil di Update" });*/
                return Ok(result);
            }
            return BadRequest(new { status = HttpStatusCode.BadRequest, result, messageResult = "Data tidak berhasil di update" });
        }

        [HttpDelete("{key}")]
        public ActionResult<Entity> Delete(Key key)
        {
            var result = repository.Delete(key);
            if (result != 0)
            {
                /*  return Ok(new { status = HttpStatusCode.OK, result, messageResult = $"Data {key} berhasil dihapus" });*/
                return Ok(result);
            }
            return NotFound(new { status = HttpStatusCode.NotFound, result, messageResult = $"Data {key} tidak ditemukan." });
        }


        [HttpGet("{key}")]
        public ActionResult Get(Key key)
        {
            var result = repository.Get(key);
            if (result != null)
            {
                /* return Ok(new { status = HttpStatusCode.OK, result, messageResult = "Data ditemukan" });*/
                return Ok(result);
            }

            return NotFound(new { status = HttpStatusCode.NotFound, result, messageResult = $"Data {key} tidak ditemukan." });
        }


    }
}
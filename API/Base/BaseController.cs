using API.Base;
using API.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;

namespace API.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repository;
        public BaseController(Repository repository)
        {
            this.repository = repository;
        }

        //MENAMPILKAN API GENERAL
        [HttpGet]
        public ActionResult<Entity> Get()
        {
            var result = repository.Get();

            return Ok(result);
        }

        //INSERT API GENERAL
        [HttpPost]
        public ActionResult<Entity> Post(Entity entity)
        {
            var result = repository.Insert(entity);
            if (result != 0)
            {
                return Ok(new { status = "SUKSES", result, message = "Data berhasil dimasukan!" });
                //return Ok(result);
            }
            return BadRequest(new { status = "GAGAL", result, message = "Data yang anda masukkan salah! Silahkan masukkan kembali data" });
        }

        //UPDATE API GENERAL BERDASARKAN PRIMARY KEY
        [HttpPut]
        public ActionResult<Entity> Put(Entity entity)
        {
            var result = repository.Update(entity);
            if (result != 0)
            {
                return Ok(result);
            }
            return BadRequest(new { status = HttpStatusCode.BadRequest, result, message = "Data gagal diupdate!" });
        }

        //DELETE API GENERAL
        [HttpDelete("{key}")]
        public ActionResult<Entity> Delete(Key key)
        {
            var result = repository.Delete(key);
            if (result != 0)
            {
                return Ok(result);
            }
            return NotFound(new { status = HttpStatusCode.NotFound, result, message = "Data Gagal dihapus!" });
        }
    }
}

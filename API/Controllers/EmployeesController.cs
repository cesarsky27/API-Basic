using API.Models;
using API.Repository;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeRepository employeeRepository;
        public EmployeesController(EmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        //Test Cors
        [HttpGet("TestCors")]
        public ActionResult TestCORS()
        {
            return Ok("TestCors berhasil");
        }

        //API Register
        [HttpPost("{Register}")]
        public ActionResult Post (RegisterVM registerVM)
        {
            var cek = employeeRepository.Register(registerVM);
            if (cek ==1)
            {
                return Conflict(new { status = "GAGAL", cek, message = "NIK sudah terdaftar!, harap gunakan NIK lain!" });
            }
            if (cek == 2)
            {
                return Conflict(new { status = "GAGAL", cek, message = "Nomor telepon yang anda masukan sudah terdaftar, masukkan nomor yang belum terdaftar!" });
            }
            if (cek == 3)
            {
                return Conflict(new { status = "GAGAL", cek, message = "Email yang anda masukan sudah terdaftar, harap masukan e-mail yang belum terdaftar" });
            }

            return Ok(new { status = "SUKSES", cek, message ="Data berhasil ditambahkan" });
        }
        //API GET
        [HttpGet]
        public ActionResult Get()
        {
            var getAllData = employeeRepository.Get().Count();
            if (getAllData != 0)
            {
                var tampilData = employeeRepository.Get();
                return Ok(new { status = HttpStatusCode.OK, tampilData });
            }
            else
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Data tidak ada yang ditampilkan, Harap masukkan data terlebih dahulu!" });
            }
        }

        //API GET BY ID
        [HttpGet("{NIK}")]
        public ActionResult Get(string NIK)
        {
            var getEmployee = employeeRepository.Get(NIK);
            if (getEmployee != null)
            {
                return Ok(getEmployee);
            }
            return NotFound($"Employee dengan NIK {NIK} tidak ditemukan!");
        }

        //API UPDATE
        [HttpPut("{NIK}")]
        public ActionResult <Employee>Put(string NIK, Employee employee)
        {
            //4
            var update = employeeRepository.Update(employee);
            if (update == 0)
            {
                return Ok(new { status = HttpStatusCode.OK, update, message = "Data berhasil diupdate" });
            }
            else if (update == 2)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, message = "No Handphone sudah terdaftar, update gagal! harap menggunakan No Handphone yang lain!" });
            }
            else if (update == 3)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, message = "E-mail sudah terdaftar, update gagal! harap menggunakan E-mail yang lain!" });
            }
            return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Data Gagal diupdate" });
        }

        //API DELETE
        [HttpDelete("{NIK}")]
        public ActionResult Delete (string NIK)
        {
            var Delete = employeeRepository.Delete(NIK);
            if (Delete != 0)
            {
                return Ok(new { status = HttpStatusCode.OK, Delete, message= $"Data Employee {NIK} berhasil dihapus"});
            }
            return NotFound(new { status = HttpStatusCode.NotFound, Delete, message = $"Data Employee {NIK} tidak ditemukan" });
        }

        //API GET ALL DATA BY NEW METHOD
        [HttpGet("GetAllData")]
        public ActionResult GetAll()
        {
            var result = employeeRepository.GetRegisteredData();
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }
    }
}

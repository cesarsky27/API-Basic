﻿using API.Base;
using API.Controllers;
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
    
    public class UniversitiesController : BaseController<University, UniversityRepository, string>
    {
        private readonly UniversityRepository universityRepository;
        public UniversitiesController(UniversityRepository universityRepository) : base (universityRepository)
        {
            this.universityRepository = universityRepository;
        }

        [HttpGet("GetTotalEmployee")]
        public ActionResult GetAll()
        {
            var result = universityRepository.GetEmpCountByUniv();
            return Ok(new { status = HttpStatusCode.OK, result, message = "Data Berhasil ditampikan" });
        }
    }

}

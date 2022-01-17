using API.Models;
using API.ViewModel;
using Client.Base.Controllers;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Authorize]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employeeRepository;
        public EmployeesController(EmployeeRepository employeeRepository) : base(employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public IActionResult Index()
        {
            return View("Employees");
        }

        [HttpGet]
        public async Task<JsonResult> GetRegister()
        {
            var result = await employeeRepository.GetRegister();
            return Json(result);
        }

        [HttpPost]
        public JsonResult Register(RegisterVM entity)
        {
            var result = employeeRepository.Register(entity);
            return Json(result);
        }
    }
}

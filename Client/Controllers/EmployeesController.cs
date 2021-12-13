using API.Models;
using API.ViewModel;
using Client.Base.Controllers;
using Client.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
  
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private EmployeeRepository employeeRepository;

        public EmployeesController(EmployeeRepository repository) : base(repository)
        {
            this.employeeRepository = repository;
        }

        [HttpGet]
        public async Task<JsonResult> GetRegister()
        {
            var result = await employeeRepository.GetRegister();
            return Json(result);
        }

        public IActionResult Index()
        {
            return View("Employees");
        }

        [HttpPost]
        public JsonResult PostRegister(RegisterVM registerVM)
        {
            var result = employeeRepository.PostRegister(registerVM);
            return Json(result);
        }

        public IActionResult Register()
        {
            return View();
        }

    }
}

using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {
        // constructor injection
        // not using the new keyword
        // readonly prevents accidentally assigning over this value
        private readonly IEmployeeRepository _employeeRepository;
        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        
        public ViewResult Index()
        {
            var model = _employeeRepository.GetAllEmployees();
            return View(model);
        }

        public ViewResult Details(int? id)
        {
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                // id??1 = if the incoming id parameter is not null, then use that value else then use default value of 1
                Employee = _employeeRepository.GetEmployee(id??1),
                PageTitle = "EmployeeDetails"
            };

            return View(homeDetailsViewModel);
        }
    }
}

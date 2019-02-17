﻿using Employees.DataLayer;
using System;
using System.Web.Mvc;
using System.Configuration;

namespace Employees.Web.Controllers
{
    public class HomeController : Controller
    {
        public static string ConnectionString;
        EmployeesRepository EmployeesRepository;
        StatusesRepository StatusesRepository;
        public HomeController()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            EmployeesRepository = new EmployeesRepository(ConnectionString);
            StatusesRepository = new StatusesRepository(ConnectionString);
        }
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.ErrorMessage = "";
            Update();
            return View();
        }
        private void Update()
        {
            ViewBag.Employees = EmployeesRepository.GetEmployees();
            ViewBag.Statuses = StatusesRepository.GetStatuses();
        }
        [HttpPost]
        public ActionResult Index(Nullable<DateTime> beginDate, Nullable<DateTime> endDate)
        {
            Session["StatusID"] = Int32.Parse(Request.Form["statusID"]);
            if (beginDate == null)
            {
                ViewBag.ErrorMessage = "Не указана начальная дата";
                Update();
                return View();
            }
            if (endDate == null)
            {
                ViewBag.ErrorMessage = "Не указана конечная дата";
                Update();
                return View();
            }
            Session["BeginDate"] = beginDate;
            Session["EndDate"] = endDate;
            return RedirectToAction("Statistics", "Statistics");
        }
    }
}
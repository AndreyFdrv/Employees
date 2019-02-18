using Employees.Model;
using Employees.DataLayer;
using System;
using System.Web.Mvc;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;

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
        public ActionResult Statistics(Nullable<DateTime> beginDate, Nullable<DateTime> endDate)
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
        public ActionResult Sort(string type, string field)
        {
            Update();
            switch (type)
            {
                case "asc":
                    switch (field)
                    {
                        case "fullName":
                            ViewBag.Employees = ((IEnumerable<Employee>)ViewBag.Employees).OrderBy(e =>
                                e.SecondName + " " + e.FirstName[0] + "." + e.LastName[0] + ".");
                            break;
                    }
                    break;
                case "desc":
                    switch (field)
                    {
                        case "fullName":
                            ViewBag.Employees = ((IEnumerable<Employee>)ViewBag.Employees).OrderByDescending(e =>
                                e.SecondName + " " + e.FirstName[0] + "." + e.LastName[0] + ".");
                            break;
                    }
                    break;
            }
            return View("Index");
        }
    }
}
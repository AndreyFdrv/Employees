using Employees.DataLayer;
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
            ViewBag.Employees = EmployeesRepository.GetEmployees();
            ViewBag.Statuses = StatusesRepository.GetStatuses();
            return View();
        }
        [HttpPost]
        public ActionResult Index(DateTime beginDate, DateTime endDate)
        {
            Session["StatusID"] = Int32.Parse(Request.Form["statusID"]);
            Session["BeginDate"] = beginDate;
            Session["EndDate"] = endDate;
            return RedirectToAction("Statistics", "Statistics");
        }
    }
}
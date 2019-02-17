using Employees.DataLayer;
using System.Web.Mvc;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace Employees.Web.Controllers
{
    public class StatisticsController : Controller
    {
        public static string ConnectionString;
        EmployeesRepository EmployeesRepository;
        public StatisticsController()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            EmployeesRepository = new EmployeesRepository(ConnectionString);
        }
        private List<DateTime> CreateDates()
        {
            var dates = new List<DateTime>();
            var beginDate = ((DateTime)Session["BeginDate"]).Date;
            var endDate = ((DateTime)Session["EndDate"]).Date;
            for (var date = beginDate; date.Date <= endDate; date = date.AddDays(1))
                dates.Add(date);
            return dates;
        }
        public ActionResult Statistics()
        {
            ViewBag.Dates = CreateDates();
            ViewBag.Counts = EmployeesRepository.GetStatistics((int)Session["StatusID"], (DateTime)Session["BeginDate"],
                (DateTime)Session["EndDate"]).ToList();
            return View();
        }
    }
}
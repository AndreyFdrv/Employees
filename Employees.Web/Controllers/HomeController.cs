using Employees.DataLayer;
using System.Web.Mvc;
using System.Configuration;

namespace Employees.Web.Controllers
{
    public class HomeController : Controller
    {
        public static string ConnectionString;
        EmployeesRepository EmployeesRepository;
        public HomeController()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            EmployeesRepository = new EmployeesRepository(ConnectionString);
        }
        public ActionResult Index()
        {
            ViewBag.Employees = EmployeesRepository.GetEmployees();
            return View();
        }
    }
}
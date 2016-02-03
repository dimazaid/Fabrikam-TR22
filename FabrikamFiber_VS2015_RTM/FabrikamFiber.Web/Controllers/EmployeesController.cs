namespace FabrikamFiber.Web.Controllers
{
    using System.Web.Mvc;
    using NLog;
    using FabrikamFiber.DAL.Data;
    using FabrikamFiber.DAL.Models;
    using System.Net.Http;
    public class EmployeesController : Controller
    {
        Logger logger = LogManager.GetLogger("FabrikamLogger");

        private readonly IEmployeeRepository employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            //comment
            this.employeeRepository = employeeRepository;
        }

        public ViewResult Index()
        {
            logger.Trace("Employee Repository");
            return View(this.employeeRepository.All);
        }

        public ViewResult Details(int id)
        {
            logger.Trace("Employee Repository view details");
            return View(this.employeeRepository.Find(id));
        }

        public ActionResult Create()
        {
            logger.Trace("Create");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                this.employeeRepository.InsertOrUpdate(employee);
                this.employeeRepository.Save();
                return RedirectToAction("Index");
            }
            var client = new HttpClient();
            client.GetAsync("www.dcsdcds.dcs");
            logger.Trace("Request made");

            return this.View();
        }

        public ActionResult Edit(int id)
        {

            return View(this.employeeRepository.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                this.employeeRepository.InsertOrUpdate(employee);
                this.employeeRepository.Save();
                return RedirectToAction("Index");
            }
            
            return this.View();
        }

        public ActionResult Delete(int id)
        {
            return View(this.employeeRepository.Find(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            this.employeeRepository.Delete(id);
            this.employeeRepository.Save();

            return RedirectToAction("Index");
        }
    }
}


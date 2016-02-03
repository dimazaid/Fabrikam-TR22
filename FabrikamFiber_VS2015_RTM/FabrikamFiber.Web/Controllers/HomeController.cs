namespace FabrikamFiber.Web.Controllers
{
    using System.Web.Mvc;

    using FabrikamFiber.DAL.Data;
    using FabrikamFiber.Web.ViewModels;

    public class HomeController : Controller
    {
        private readonly IServiceTicketRepository serviceTickets;
        private readonly IMessageRepository messageRepository;
        private readonly IAlertRepository alertRepository;
        private readonly IScheduleItemRepository scheduleItemRepository;

        public HomeController(
                              IServiceTicketRepository serviceTickets,
                              IMessageRepository messageRepository,
                              IAlertRepository alertRepository,
                              IScheduleItemRepository scheduleItemRepository)
        {
            this.serviceTickets = serviceTickets;
            this.messageRepository = messageRepository;
            this.alertRepository = alertRepository;
            this.scheduleItemRepository = scheduleItemRepository;
        }

        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            var viewModel = new DashboardViewModel
            {
                ScheduleItems = this.scheduleItemRepository.All,
                Messages = this.messageRepository.All,
                Alerts = this.alertRepository.All,
                Tickets = this.serviceTickets.AllIncluding(serviceticket => serviceticket.Customer, serviceticket => serviceticket.CreatedBy, serviceticket => serviceticket.AssignedTo),
            };


            try
            {
                var client = new System.Net.Http.HttpClient();
                var response = await client.GetAsync("http://www2.bing.com");

                if (!response.IsSuccessStatusCode)
                {
                    // handle the second type of error (404, 400, etc.)
                }
            }
            catch (HttpRequestException ex)
            {
                // handle the first type of error (no connectivity, etc)
            }
            return View(viewModel);
        }
    }
}

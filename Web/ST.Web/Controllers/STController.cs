using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ST.SharedEntitiesLib;
using ST.SharedInterfacesLib;
using ST.Web.Models.ST;

namespace ST.Web.Controllers
{
    //[Authorize]
    public class STController : Controller
    {
        private readonly ISTAppService<ISTRepo> _stService;

        public STController(ISTAppService<ISTRepo> stService)
        {
            _stService = stService;
            Initialise();
        }

        private void Initialise()
        {
            // TODO - Configure this using the ASP.Net Core Configuration way.
            ViewBag.Environment = "TODO-Environment";
            // ViewBag.Environment = ConfigurationManager<>.AppSettings["Environment"];
        }

        public ActionResult Index()
        {
            var tickets = _stService.GetActiveTickets();

            return View(tickets);
        }
        
        [HttpPost, ValidateAntiForgeryToken]
        [Route("/AddTicket")]
        public ActionResult AddTicket([Bind(Prefix = "Ticket.ProductId")]int productId, [Bind(Prefix = "Ticket.SeverityId")]int severityId, [Bind(Prefix = "Ticket.Problem")]string problem, [Bind(Prefix = "Ticket.Description")]string description, bool active)
        {
            if (!ModelState.IsValid)
            {
                // TODO: Do more granular checks per parameter to find out what is wrong...
                ModelState.AddModelError("OneOfTheAboveParameters", "OneOfTheAboveParameters has a problem.");
                ViewBag.Message = "A critical error occured in the model binding";
                return View(new STAddTicketViewModel());
            }

            var ticket = _stService.AddTicket(
                new Ticket()
                {
                    ProductId = productId,
                    SeverityId = severityId,
                    Problem = problem,
                    Description = description,
                    Active = active
                });

            ViewBag.Message = $"Your ticket has been created.  Your ticket ID is {ticket.TicketId}";

            STAddTicketViewModel vm = GetAddTicketVm(new Ticket());

            return View(vm);
        }

        [Route("/AddTicket")]
        public ActionResult AddTicket()
        {
            var ticket = new Ticket();

            STAddTicketViewModel vm = GetAddTicketVm(ticket);

            return View(vm);
        }

        private STAddTicketViewModel GetAddTicketVm(Ticket ticket)
        {
            var severities = _stService.GetSeverities();
            var severitiesSelectItem = severities.Select(s => new SelectListItem()
            {
                Value = s.SeverityId.ToString(),
                Text = s.DisplayName
            });

            var products = _stService.GetProducts();
            var productsSelectItem = products.Select(p => new SelectListItem()
            {
                Value = p.ProductId.ToString(),
                Text = p.Description
            });

            var vm = new STAddTicketViewModel()
            {
                Severities = severitiesSelectItem,
                Products = productsSelectItem,
                Ticket = ticket
            };



            return vm;
        }

        //TODO [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult UpdateStatus(string status)
        {
            // TODO - Move the STSession into another namespace and take away the dependency between this and the API controllers
            // STSession.Status = status;
            return Ok(status);  // TODO - Fix the status
        }
    }
}
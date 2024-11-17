using Microsoft.AspNetCore.Mvc;

namespace FrontierHackUTDEntry.Controllers.Support
{
    public class SupportController : Controller
    {
        [HttpGet]
        public IActionResult Index(string ticketId)
        {
            ViewData["TicketId"] = ticketId; // Pass variable to the view
            return View();
        }
    }
}

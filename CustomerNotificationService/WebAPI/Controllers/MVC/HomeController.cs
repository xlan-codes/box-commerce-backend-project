using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebAPI.Models;

namespace WebAPI.Controllers.MVC
{
    public class HomeController : Controller
    {
        private readonly OIDCSettings _oidcSettings;
        private readonly IMediator _mediator;

        public HomeController(IOptions<OIDCSettings> oidcSettings, IMediator mediator)
        {
            _mediator = mediator;
            _oidcSettings = oidcSettings.Value;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(_oidcSettings);
        }

        [Route("status")]
        [HttpGet]
        public ActionResult Status()
        {
            return View("status");
        }
    }
}

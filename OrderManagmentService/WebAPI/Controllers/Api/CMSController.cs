using Application.Generics.Dtos.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Api
{
    [ApiController]
    [Route("api/cms")]
    public class CMSController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CMSController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
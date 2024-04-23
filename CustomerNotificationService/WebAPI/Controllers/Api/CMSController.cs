using Application.Generics.Dtos.Response;
using Application.UseCases.CMSModule.Commands;
using Application.UseCases.CMSModule.Dtos;
using Application.UseCases.CMSModule.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Filters;

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
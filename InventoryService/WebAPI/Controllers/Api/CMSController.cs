using Application.Generics.Dtos.ResponseDtos;
using Application.UseCases.CMSModule.Commands;
using Application.UseCases.CMSModule.Dtos;
using Application.UseCases.CMSModule.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
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

        [Authorize]
        [HttpGet]
        [Route("flow")]
        [ProducesResponseType(200, Type = typeof(PaginationDto<IEnumerable<AgentFlowsDto>>))]
        public async Task<ActionResult> GetAgentFlows([FromQuery] GetAgentFlowQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        [Route("flow/{id}")]
        [ProducesResponseType(200, Type = typeof(SingleAgentFlowsDto))]
        public async Task<ActionResult> GetAgentFlow(string id)
        {
            var response = await _mediator.Send(new GetSingleAgentFlowQuery() { Id = id });
            return Ok(response);
        }

        [CmsExportAuthorization]
        [HttpGet("export")]
        public async Task<object> GenerateAgentFlowCSV()
        {

            Response.Headers.Add("Content-Disposition", "attachment;filename=ClientFlows.csv");

            Response.ContentType = "text/csv";

            await _mediator.Send(new StreamClientFlowCsvCommand() { ClientCsvStream = Response.BodyWriter.AsStream() });

            await Response.CompleteAsync();

            return new EmptyResult();
        }
    }
}
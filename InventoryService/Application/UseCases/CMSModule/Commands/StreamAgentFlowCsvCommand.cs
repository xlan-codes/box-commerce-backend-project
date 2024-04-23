using Application.Contracts.Persistence;
using Application.Generics.Dtos;
using MediatR;

namespace Application.UseCases.CMSModule.Commands
{
    #region Query
    public class StreamClientFlowCsvCommand : Identity, IRequest<object>
    {
        public Stream ClientCsvStream { get; set; }
    }
    #endregion

}

using Application.Contracts.Persistence;
using Application.Generics.Dtos;
using Application.Generics.Dtos.Settings;
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

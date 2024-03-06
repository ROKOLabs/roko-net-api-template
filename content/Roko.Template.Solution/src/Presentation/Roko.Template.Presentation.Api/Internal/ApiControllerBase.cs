namespace Roko.Template.Presentation.Api.Internal
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Roko.Template.Domain;
    using System.Net.Mime;
    using System.Threading.Tasks;

    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected ApiControllerBase(
            IMediator mediator)
        {
            this.Mediator = mediator;
        }

        public IMediator Mediator { get; }

        protected async Task<IActionResult> ProcessAsync<TCommand, TResponse>(
            TCommand command)
            where TCommand : IRequest<TResponse>
        {
            return this.Ok(await this.Mediator.Send(command)); 
        }

        protected async Task<IActionResult> ProcessCreateAsync<TCommand, TResponse>(
            TCommand command)
            where TCommand : IRequest<TResponse>
            where TResponse : IResource
        {
            TResponse response = await this.Mediator.Send(command);

            return this.Created($"{this.Request.Path}/{response.Id}", response);
        }
    }
}
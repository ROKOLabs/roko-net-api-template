namespace Roko.Template.Presentation.Api.Internal
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
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
            TResponse result = await this.Mediator.Send(command);

            if (result is null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }

        protected async Task<IActionResult> ProcessAsync<TCommand>(
            TCommand command)
            where TCommand : IRequest
        {
            await this.Mediator.Send(command);

            return this.NoContent();
        }
    }
}
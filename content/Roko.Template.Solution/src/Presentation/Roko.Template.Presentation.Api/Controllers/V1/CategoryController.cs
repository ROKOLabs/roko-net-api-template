namespace Roko.Template.Presentation.Api.Controllers.V1
{
    using Asp.Versioning;
    using Roko.Template.Application.Categories;
    using Roko.Template.Domain;
    using Roko.Template.Presentation.Api.Internal;
    using Roko.Template.Presentation.Api.Internal.Constants;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [ApiVersion(ApiVersions.V1)]
    internal class CategoryController : ApiControllerBase
    {
        public CategoryController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCategoryCommand command)
        {
            return await this.ProcessAsync(command);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateCategoryCommand command)
        {
            return await this.ProcessAsync(command);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetCategoriesQuery command)
        {
            return await this.ProcessAsync<GetCategoriesQuery, List<Category>>(command);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return await this.ProcessAsync<GetCategoryByIdQuery, Category>(new GetCategoryByIdQuery(id));
        }
    }
}

// using System.Net.Mime;
// using JacksonVeroneze.TemplateWebApi.Api.Extensions;
// using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
// using JacksonVeroneze.TemplateWebApi.Application.Queries.Client;
// using MediatR;
// using Microsoft.AspNetCore.Mvc;
//
// namespace JacksonVeroneze.TemplateWebApi.Api.Controllers.v1;
//
// [ApiController]
// [ApiVersion("1.0")]
// [Route("/api/v{version:apiVersion}/clients")]
// [Produces(MediaTypeNames.Application.Json)]
// public class ClientsController : ControllerBase
// {
//     private readonly ILogger<ClientsController> _logger;
//     private readonly ISender _mediator;
//
//     public ClientsController(
//         ILogger<ClientsController> logger,
//         ISender mediator)
//     {
//         _logger = logger;
//         _mediator = mediator;
//     }
//
//     [HttpGet(Name = "GetPagedClients")]
//     [ApiConventionMethod(typeof(DefaultApiConventions),
//         nameof(DefaultApiConventions.Get))]
//     public async Task<IActionResult> GetPagedAsync(
//         GetClientPagedQuery query,
//         CancellationToken cancellationToken)
//     {
//         _logger.LogGetPaged(nameof(ClientsController),
//             nameof(GetPagedAsync));
//
//         BaseResponse response = await _mediator
//             .Send(query, cancellationToken);
//
//         return StatusCode((int)response.Status, response);
//     }
//
//     [HttpGet("{id}", Name = "GetByIdClient")]
//     [ApiConventionMethod(typeof(DefaultApiConventions),
//         nameof(DefaultApiConventions.Find))]
//     public async Task<IActionResult> GetByIdAsync(
//         [FromRoute] GetClientByIdQuery query,
//         CancellationToken cancellationToken)
//     {
//         _logger.LogGetById(nameof(ClientsController),
//             nameof(GetByIdAsync), query.Id);
//
//         BaseResponse response = await _mediator
//             .Send(query, cancellationToken);
//
//         return StatusCode((int)response.Status, response);
//     }
// }




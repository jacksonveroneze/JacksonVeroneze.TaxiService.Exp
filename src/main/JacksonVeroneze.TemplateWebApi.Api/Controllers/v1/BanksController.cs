using System.Net.Mime;
using JacksonVeroneze.TemplateWebApi.Api.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Commands.Bank;
using JacksonVeroneze.TemplateWebApi.Application.Models.Bank;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Application.Queries.Bank;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("/api/v{version:apiVersion}/banks")]
[Produces(MediaTypeNames.Application.Json)]
public class BanksController : ControllerBase
{
    private readonly ILogger<BanksController> _logger;
    private readonly ISender _mediator;

    public BanksController(
        ILogger<BanksController> logger,
        ISender mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet(Name = "GetPagedBanks")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Get))]
    public async Task<IActionResult> GetPagedAsync(
        GetBankPagedQuery query,
        CancellationToken cancellationToken)
    {
        _logger.LogGetPaged(nameof(BanksController),
            nameof(GetPagedAsync));

        BaseResponse response = await _mediator
            .Send(query, cancellationToken);

        return StatusCode((int)response.Status, response);
    }

    [HttpGet("{id}", Name = "GetByIdBank")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Find))]
    public async Task<IActionResult> GetByIdAsync(
        [FromRoute] GetBankByIdQuery query,
        CancellationToken cancellationToken)
    {
        _logger.LogGetById(nameof(BanksController),
            nameof(GetByIdAsync), query.Id);

        BaseResponse response = await _mediator
            .Send(query, cancellationToken);

        return StatusCode((int)response.Status, response);
    }

    [HttpPost(Name = "CreateBank")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Create))]
    public async Task<IActionResult> CreateAsync(
        [FromBody] CreateBankCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogCreate(nameof(BanksController),
            nameof(GetByIdAsync));

        CreateBankCommandResponse response =
            (CreateBankCommandResponse)await _mediator
                .Send(command, cancellationToken);

        return response.Status is ResponseStatus.Created
            ? CreatedAtRoute("GetByIdBank",
                new { id = response.Data.Id }, response)
            : StatusCode((int)response.Status, response);
    }

    [HttpDelete("{id}", Name = "DeleteBank")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Delete))]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] DeleteBankCommand query,
        CancellationToken cancellationToken)
    {
        _logger.LogGetById(nameof(BanksController),
            nameof(GetByIdAsync), query.Id);

        BaseResponse response = await _mediator
            .Send(query, cancellationToken);

        return response.Status is ResponseStatus.NoContent
            ? NoContent()
            : StatusCode((int)response.Status, response);
    }
}

using System.Net.Mime;
using JacksonVeroneze.TemplateWebApi.Api.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Commands.Bank;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Application.Primitives;
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
        // _logger.LogGetPaged(nameof(BanksController),
        //     nameof(GetPagedAsync));

        Result<BaseResponse> response = await _mediator
            .Send(query, cancellationToken);

        return response.MatchGet(this);
    }

    [HttpGet("{id}", Name = "FindById")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Find))]
    public async Task<IActionResult> FindByIdAsync(
        [FromRoute] GetBankByIdQuery query,
        CancellationToken cancellationToken)
    {
        // _logger.LogGetById(nameof(BanksController),
        //     nameof(FindByIdAsync), query.Id);

        Result<BaseResponse> response = await _mediator
            .Send(query, cancellationToken);

        return response.MatchFind(this);
    }

    [HttpPost(Name = "CreateBank")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Create))]
    public async Task<IActionResult> CreateAsync(
        [FromBody] CreateBankCommand command,
        CancellationToken cancellationToken)
    {
        // _logger.LogCreate(nameof(BanksController),
        //     nameof(GetByIdAsync));

        Result<BaseResponse> response = await _mediator
            .Send(command, cancellationToken);

        return response.MatchPost(this);
    }

    [HttpDelete("{id}", Name = "DeleteBank")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Delete))]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] DeleteBankCommand query,
        CancellationToken cancellationToken)
    {
        // _logger.LogGetById(nameof(BanksController),
        //     nameof(GetByIdAsync), query.Id);

        Result<BaseResponse> response = await _mediator
            .Send(query, cancellationToken);

        return response.MatchDelete(this);
    }

    [HttpPut("{id}/activate", Name = "ActivateBank")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Put))]
    public async Task<IActionResult> PutActivateAsync(
        [FromRoute] ActivateBankCommand query,
        CancellationToken cancellationToken)
    {
        // _logger.LogGetById(nameof(BanksController),
        //     nameof(GetByIdAsync), query.Id);

        Result<BaseResponse> response = await _mediator
            .Send(query, cancellationToken);

        return response.MatchPut(this);
    }
}

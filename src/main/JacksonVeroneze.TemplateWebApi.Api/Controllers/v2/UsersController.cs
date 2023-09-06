using System.Net.Mime;
using AutoMapper;
using JacksonVeroneze.NET.DomainObjects.Result;
using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Api.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Commands.User;
using JacksonVeroneze.TemplateWebApi.Application.Commands.User.Email;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base;
using JacksonVeroneze.TemplateWebApi.Application.Models.User;
using JacksonVeroneze.TemplateWebApi.Application.Models.User.Email;
using JacksonVeroneze.TemplateWebApi.Application.Queries.User;
using JacksonVeroneze.TemplateWebApi.Application.Queries.User.Email;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Api.Controllers.v2;

[ApiController]
[ApiVersion("2.0")]
[Route("/api/v{version:apiVersion}/users")]
[Produces(MediaTypeNames.Application.Json)]
public sealed class UsersController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserReadRepository _userReadRepository;

    public UsersController(
        IMapper mapper,
        IUserReadRepository userReadRepository)
    {
        _mapper = mapper;
        _userReadRepository = userReadRepository;
    }

    [HttpGet]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Get))]
    public async Task<IActionResult> GetPagedAsync(
        GetUserPagedQuery query,
        CancellationToken cancellationToken)
    {
        UserPagedFilter filter = new()
        {
            Pagination = new PaginationParameters(
                query.Page!.Value, query.PageSize!.Value)
        };

        Page<UserEntity> data = await _userReadRepository.GetPagedAsync(
            filter, cancellationToken);

        GetUserPagedQueryResponse? result =
            _mapper.Map<GetUserPagedQueryResponse>(data);

        return Ok(result);
    }
}

using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Interfaces.Services.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.CommandHandler.User;

public sealed class DeleteUserCommandHandler(
    IDeleteUserService service)
    : IRequestHandler<DeleteUserCommand, IResult<VoidResponse>>
{
    public async Task<IResult<VoidResponse>> Handle(
        DeleteUserCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        IResult result = await service
            .DeleteAsync(request.Id, cancellationToken);

        return result.IsSuccess
            ? Result<VoidResponse>.Success()
            : Result<VoidResponse>.Invalid(result.Error!);
    }
}

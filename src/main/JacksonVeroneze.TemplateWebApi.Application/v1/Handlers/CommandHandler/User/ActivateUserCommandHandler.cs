using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Interfaces.Services.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.CommandHandler.User;

public sealed class ActivateUserCommandHandler(
    IActivateUserService service)
    : IRequestHandler<ActivateUserCommand, IResult<VoidResponse>>
{
    public async Task<IResult<VoidResponse>> Handle(
        ActivateUserCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        IResult result = await service
            .ActivateAsync(request.Id, cancellationToken);

        return result.IsSuccess
            ? Result<VoidResponse>.Success()
            : Result<VoidResponse>.Invalid(result.Error!);
    }
}

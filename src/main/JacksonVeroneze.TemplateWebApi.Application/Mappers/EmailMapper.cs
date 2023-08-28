using JacksonVeroneze.TemplateWebApi.Application.Models.User.Email;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.Mappers;

public class EmailMapper : Profile
{
    public EmailMapper()
    {
        // Entity -> Response
        CreateMap<EmailEntity, EmailResponse>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email));

        CreateMap<IReadOnlyCollection<EmailEntity>, GetAllEmailsByUserIdQueryResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src));
    }
}

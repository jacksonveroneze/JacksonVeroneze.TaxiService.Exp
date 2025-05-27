using TaxiService.Modules.Passengers.Application.v1.Email.Create;
using TaxiService.Modules.Passengers.Application.v1.Email.GetPaged;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.Domain.Filters;

namespace TaxiService.Modules.Passengers.Application.v1.Email;

public class EmailMapper : Profile
{
    public EmailMapper()
    {
        // Entity -> Response
        CreateMap<EmailEntity, EmailResponse>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email));

        CreateMap<EmailEntity, CreateEmailCommandResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src));

        CreateMap<ICollection<EmailEntity>, GetEmailPagedQueryResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src));

        // Query -> Filter
        CreateMap<GetEmailPagedQuery, EmailPagedFilter>()
            .ForMember(dest => dest.PassengerId, opts => opts.MapFrom(src => src.PassengerId))
            .ForMember(dest => dest.Pagination, opts => opts.MapFrom(src => src));
    }
}

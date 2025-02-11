using TaxiService.Modules.Passengers.Application.v1.Models.Email;
using TaxiService.Modules.Passengers.Application.v1.Queries.Passanger.Email;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.Domain.Filters;

namespace TaxiService.Modules.Passengers.Application.v1.Mappers;

public class EmailMapper : Profile
{
    public EmailMapper()
    {
        // Entity -> Response
        CreateMap<EmailEntity, EmailResponse>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email));

        CreateMap<EmailEntity, CreateCommandResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src));

        CreateMap<ICollection<EmailEntity>, GetPagedQueryResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src));

        // Query -> Filter
        CreateMap<GetPagedQuery, EmailPagedFilter>()
            .ForMember(dest => dest.PassangerId, opts => opts.MapFrom(src => src.PassangerId))
            .ForMember(dest => dest.Pagination, opts => opts.MapFrom(src => src));
    }
}
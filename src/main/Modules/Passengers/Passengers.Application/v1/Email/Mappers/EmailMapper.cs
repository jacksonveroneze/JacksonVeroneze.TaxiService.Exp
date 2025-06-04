using TaxiService.Modules.Passengers.Application.v1.Email.Common.Filters;
using TaxiService.Modules.Passengers.Application.v1.Email.Common.Models;
using TaxiService.Modules.Passengers.Application.v1.Email.Create;
using TaxiService.Modules.Passengers.Application.v1.Email.GetPaged;
using TaxiService.Modules.Passengers.Application.v1.Passenger.Common.Filters;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Application.v1.Email.Mappers;

public class EmailMapper : Profile
{
    public EmailMapper()
    {
        // Entity -> Response
        CreateMap<Domain.Entities.Email, EmailResponse>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Value));

        CreateMap<Domain.Entities.Email, CreateEmailCommandResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src));

        CreateMap<ICollection<Domain.Entities.Email>, GetEmailPagedQueryResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src));

        // Query -> Filter
        CreateMap<GetEmailPagedQuery, EmailPagedFilter>()
            .ForMember(dest => dest.PassengerId, opts => opts.MapFrom(src => src.PassengerId))
            .ForMember(dest => dest.Pagination, opts => opts.MapFrom(src => src));
    }
}

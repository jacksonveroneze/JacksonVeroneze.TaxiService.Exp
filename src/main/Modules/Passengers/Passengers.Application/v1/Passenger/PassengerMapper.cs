using JacksonVeroneze.NET.Pagination.Cursor;

using TaxiService.Modules.Passengers.Application.v1.Passenger.Create;
using TaxiService.Modules.Passengers.Application.v1.Passenger.GetById;
using TaxiService.Modules.Passengers.Application.v1.Passenger.GetPaged;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.Domain.Filters;

namespace TaxiService.Modules.Passengers.Application.v1.Passenger;

public class PassengerMapper : Profile
{
    public PassengerMapper()
    {
        // Entity -> Response
        CreateMap<PassengerEntity, PassengerResponse>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.FullName, opts => opts.MapFrom(src => src.Name.Value))
            .ForMember(dest => dest.Document, opts => opts.MapFrom(src => src.Cpf.Value))
            .ForMember(dest => dest.Birthday, opts => opts.MapFrom(src => src.Birthday))
            .ForMember(dest => dest.Gender, opts => opts.MapFrom(src => src.Gender))
            .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status));

        CreateMap<PassengerEntity, GetPassengerByIdQueryResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src));

        CreateMap<PassengerEntity, CreatePassengerCommandResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src));

        CreateMap<Page<PassengerEntity>, GetPassengerPagedQueryResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src.Data))
            .ForMember(dest => dest.Pagination, opts => opts.MapFrom(src => src.PageInfo));

        // Query -> Filter
        CreateMap<GetPassengerPagedQuery, PassengerPagedFilter>()
            .ForMember(dest => dest.PassengerId, opts => opts.Ignore())
            .ForMember(dest => dest.Pagination, opts => opts.MapFrom(src => src));
    }
}

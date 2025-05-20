using JacksonVeroneze.NET.Pagination.Cursor;
using TaxiService.Modules.Passengers.Application.v1.Models.Passenger;
using TaxiService.Modules.Passengers.Application.v1.Queries.Passenger;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.Domain.Filters;

namespace TaxiService.Modules.Passengers.Application.v1.Mappers;

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

        CreateMap<PassengerEntity, GetByIdQueryResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src));

        CreateMap<PassengerEntity, CreateCommandResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src));

        CreateMap<Page<PassengerEntity>, GetPagedQueryResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src.Data))
            .ForMember(dest => dest.Pagination, opts => opts.MapFrom(src => src.PageInfo));

        // Query -> Filter
        CreateMap<GetPagedQuery, PassengerPagedFilter>()
            .ForMember(dest => dest.PassengerId, opts => opts.Ignore())
            .ForMember(dest => dest.Pagination, opts => opts.MapFrom(src => src));
    }
}

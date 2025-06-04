using JacksonVeroneze.NET.Pagination.Cursor;
using TaxiService.Modules.Passengers.Application.v1.Passenger.Common.Filters;
using TaxiService.Modules.Passengers.Application.v1.Passenger.Common.Models;
using TaxiService.Modules.Passengers.Application.v1.Passenger.Create;
using TaxiService.Modules.Passengers.Application.v1.Passenger.GetById;
using TaxiService.Modules.Passengers.Application.v1.Passenger.GetPaged;
using TaxiService.Modules.Passengers.PublicApi.v1.Models;

namespace TaxiService.Modules.Passengers.Application.v1.Passenger.Mappers;

public class PassengerMapper : Profile
{
    public PassengerMapper()
    {
        // Entity -> Response
        CreateMap<Domain.Aggregates.Passenger, PassengerResponse>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.FullName, opts => opts.MapFrom(src => src.Name.Value))
            .ForMember(dest => dest.Document, opts => opts.MapFrom(src => src.Cpf.Value))
            .ForMember(dest => dest.Birthday, opts => opts.MapFrom(src => src.Birthday))
            .ForMember(dest => dest.Gender, opts => opts.MapFrom(src => src.Gender))
            .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status));

        CreateMap<PassengerDto, GetPassengerByIdQueryResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src));

        CreateMap<Domain.Aggregates.Passenger, CreatePassengerCommandResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src));

        CreateMap<Page<PassengerDto>, GetPassengerPagedQueryResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src.Data))
            .ForMember(dest => dest.Pagination, opts => opts.MapFrom(src => src.PageInfo));

        // Query -> Filter
        CreateMap<GetPassengerPagedQuery, PassengerPagedFilter>()
            .ForMember(dest => dest.Pagination, opts => opts.MapFrom(src => src));

        CreateMap<PassengerDto, PassengerResponse>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.FullName, opts => opts.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Document, opts => opts.MapFrom(src => src.Document))
            .ForMember(dest => dest.Birthday, opts => opts.MapFrom(src => src.Birthday))
            .ForMember(dest => dest.Gender, opts => opts.MapFrom(src => src.Gender))
            .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status));

        CreateMap<PassengerDto, PassengerPublicApiDto>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.FullName, opts => opts.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Document, opts => opts.MapFrom(src => src.Document))
            .ForMember(dest => dest.Birthday, opts => opts.MapFrom(src => src.Birthday))
            .ForMember(dest => dest.Gender, opts => opts.MapFrom(src => src.Gender))
            .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status));
    }
}

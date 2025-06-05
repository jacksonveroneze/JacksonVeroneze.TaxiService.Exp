using JacksonVeroneze.NET.Pagination.Cursor;
using TaxiService.Modules.Ride.Application.v1.Ride.Common.Filters;
using TaxiService.Modules.Ride.Application.v1.Ride.Common.Models;
using TaxiService.Modules.Ride.Application.v1.Ride.GetById;
using TaxiService.Modules.Ride.Application.v1.Ride.GetPaged;
using TaxiService.Modules.Ride.Application.v1.Ride.Request;

namespace TaxiService.Modules.Ride.Application.v1.Ride.Mappers;

public class RideMapper : Profile
{
    public RideMapper()
    {
        // Entity -> Response
        CreateMap<Domain.Aggregates.Ride, RideResponse>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.PassengerId, opts => opts.MapFrom(src => src.PassengerId))
            .ForMember(dest => dest.DriverId, opts => opts.MapFrom(src => src.DriverId))
            .ForMember(dest => dest.Fare, opts => opts.MapFrom(src => src.Fare))
            .ForMember(dest => dest.Distance, opts => opts.MapFrom(src => src.Distance))
            .ForMember(dest => dest.From, opts => opts.MapFrom(src => src.CoordinateFrom))
            .ForMember(dest => dest.To, opts => opts.MapFrom(src => src.CoordinateTo))
            .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status));

        CreateMap<RideDto, GetRideByIdQueryResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src));

        CreateMap<Domain.Aggregates.Ride, RequestRideCommandResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src));

        CreateMap<Page<RideDto>, GetRidePagedQueryResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src.Data))
            .ForMember(dest => dest.Pagination, opts => opts.MapFrom(src => src.PageInfo));

        // Query -> Filter
        CreateMap<GetRidePagedQuery, RidePagedFilter>()
            .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status))
            .ForMember(dest => dest.PassengerId, opts => opts.MapFrom(src => src.PassengerId))
            .ForMember(dest => dest.Pagination, opts => opts.MapFrom(src => src));

        CreateMap<RideDto, RideResponse>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.PassengerId, opts => opts.MapFrom(src => src.PassengerId))
            .ForMember(dest => dest.DriverId, opts => opts.MapFrom(src => src.DriverId))
            .ForMember(dest => dest.Fare, opts => opts.MapFrom(src => src.Fare))
            .ForMember(dest => dest.Distance, opts => opts.MapFrom(src => src.Distance))
            .ForMember(dest => dest.From, opts => opts.MapFrom(src => src.From))
            .ForMember(dest => dest.To, opts => opts.MapFrom(src => src.To))
            .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status));
    }
}

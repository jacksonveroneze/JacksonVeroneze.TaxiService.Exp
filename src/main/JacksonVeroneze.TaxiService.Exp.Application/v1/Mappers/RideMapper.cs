using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Ride;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.Ride;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Filters;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Mappers;

public class RideMapper : Profile
{
    public RideMapper()
    {
        // Entity -> Response
        CreateMap<RideEntity, RideResponse>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserId, opts => opts.Ignore())
            .ForMember(dest => dest.DriverId, opts => opts.Ignore())
            .ForMember(dest => dest.Fare, opts => opts.MapFrom(src => src.Fare))
            .ForMember(dest => dest.Distance, opts => opts.MapFrom(src => src.Distance))
            .ForMember(dest => dest.From, opts => opts.MapFrom(src => src.From))
            .ForMember(dest => dest.To, opts => opts.MapFrom(src => src.To))
            .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status));

        CreateMap<RideEntity, GetRideByIdQueryResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src));

        CreateMap<RideEntity, RequestRideCommandResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src));

        CreateMap<Page<RideEntity>, GetRidePagedQueryResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src.Data))
            .ForMember(dest => dest.Pagination, opts => opts.MapFrom(src => src.Pagination));

        // Query -> Filter
        CreateMap<GetRidePagedQuery, RidePagedFilter>()
            .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status))
            .ForMember(dest => dest.UserId, opts => opts.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Pagination, opts => opts.MapFrom(src => src));
    }
}

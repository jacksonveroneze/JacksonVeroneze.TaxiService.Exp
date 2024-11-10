using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Ride;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.Ride;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Filters;
using JacksonVeroneze.TaxiService.Exp.Domain.Results;

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
            .ForMember(dest => dest.From, opts => opts.MapFrom(src => src.CoordinateFrom))
            .ForMember(dest => dest.To, opts => opts.MapFrom(src => src.CoordinateTo))
            .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status));

        CreateMap<RideEntity, GetRideByIdQueryResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src));

        CreateMap<RideEntity, RequestRideCommandResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src));

        CreateMap<PaginatedResult<RideEntity>, GetRidePagedQueryResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src.Data))
            .ForPath(dest => dest.Pagination!.Next, opts => opts.MapFrom(src => src.Cursor))
            .ForPath(dest => dest.Pagination!.HasMore, opts => opts.MapFrom(src => src.HasMore));

        // Query -> Filter
        CreateMap<GetRidePagedQuery, RidePagedFilter>()
            .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status))
            .ForMember(dest => dest.UserId, opts => opts.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Pagination, opts => opts.MapFrom(src => src))
            .ForMember(dest => dest.CursorPagination, opts => opts.Ignore());
    }
}
using TaxiService.Common.BaseApplication.Models.Models.Common;
using TaxiService.Common.BaseDomain.ValueObjects;

namespace TaxiService.Modules.Rides.Application.v1.Mappers;

public class CoordinateMapper : Profile
{
    public CoordinateMapper()
    {
        // ValueObject -> Response
        CreateMap<CoordinateValueObject, CoordinateResponse>()
            .ForMember(dest => dest.Latitude, opts => opts.MapFrom(src => src.Latitude))
            .ForMember(dest => dest.Longitude, opts => opts.MapFrom(src => src.Longitude));
    }
}
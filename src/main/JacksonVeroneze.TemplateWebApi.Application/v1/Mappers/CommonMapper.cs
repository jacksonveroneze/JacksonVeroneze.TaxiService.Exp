using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Mappers;

public class CommonMapper : Profile
{
    public CommonMapper()
    {
        // ValueObject -> Response
        CreateMap<CoordinateValueObject, CoordinateResponse>()
            .ForMember(dest => dest.Latitude, opts => opts.MapFrom(src => src.Latitude))
            .ForMember(dest => dest.Longitude, opts => opts.MapFrom(src => src.Longitude));
    }
}

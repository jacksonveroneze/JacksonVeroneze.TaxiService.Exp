using TaxiService.Modules.Drivers.Application.v1.Models.Driver;
using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.v1.Mappers;

public class DriverMapper : Profile
{
    public DriverMapper()
    {
        // Entity -> Response
        CreateMap<DriverEntity, DriverResponse>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.FullName, opts => opts.MapFrom(src => src.Name.Value))
            .ForMember(dest => dest.Document, opts => opts.MapFrom(src => src.Cpf.Value))
            .ForMember(dest => dest.Birthday, opts => opts.MapFrom(src => src.Birthday))
            .ForMember(dest => dest.Gender, opts => opts.MapFrom(src => src.Gender))
            .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status));

        CreateMap<DriverEntity, CreateCommandResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src));
    }
}
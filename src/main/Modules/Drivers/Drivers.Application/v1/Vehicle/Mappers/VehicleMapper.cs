using TaxiService.Modules.Drivers.Application.v1.Vehicle.Common;
using TaxiService.Modules.Drivers.Application.v1.Vehicle.Common.Models;
using TaxiService.Modules.Drivers.Domain.Entities;

using CreateCommandResponse =
    TaxiService.Modules.Drivers.Application.v1.Vehicle.Create.CreateCommandResponse;

namespace TaxiService.Modules.Drivers.Application.v1.Vehicle.Mappers;

public class VehicleMapper : Profile
{
    public VehicleMapper()
    {
        // Entity -> Response
        CreateMap<VehicleEntity, VehicleResponse>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.DriverId, opts => opts.MapFrom(src => src.DriverId))
            .ForMember(dest => dest.Model, opts => opts.MapFrom(src => src.Model))
            .ForMember(dest => dest.Brand, opts => opts.MapFrom(src => src.Brand))
            .ForMember(dest => dest.Plate, opts => opts.MapFrom(src => src.Plate))
            .ForMember(dest => dest.Color, opts => opts.MapFrom(src => src.Color))
            .ForMember(dest => dest.Year, opts => opts.MapFrom(src => src.Year))
            .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status));

        CreateMap<VehicleEntity, CreateCommandResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src));
    }
}

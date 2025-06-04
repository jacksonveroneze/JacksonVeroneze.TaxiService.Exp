using JacksonVeroneze.NET.Pagination.Cursor;
using TaxiService.Modules.Drivers.Application.v1.Driver.Common.Filters;
using TaxiService.Modules.Drivers.Application.v1.Driver.Common.Models;
using TaxiService.Modules.Drivers.Application.v1.Driver.Create;
using TaxiService.Modules.Drivers.Application.v1.Driver.GetById;
using TaxiService.Modules.Drivers.Application.v1.Driver.GetPaged;
using TaxiService.Modules.Drivers.PublicApi.v1.Models;

namespace TaxiService.Modules.Drivers.Application.v1.Driver.Mappers;

public class DriverMapper : Profile
{
    public DriverMapper()
    {
        // Entity -> Response
        CreateMap<Domain.Aggregates.Driver, DriverResponse>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.FullName, opts => opts.MapFrom(src => src.Name.Value))
            .ForMember(dest => dest.Document, opts => opts.MapFrom(src => src.Cpf.Value))
            .ForMember(dest => dest.Birthday, opts => opts.MapFrom(src => src.Birthday))
            .ForMember(dest => dest.Gender, opts => opts.MapFrom(src => src.Gender))
            .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status));

        CreateMap<DriverDto, DriverResponse>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.FullName, opts => opts.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Document, opts => opts.MapFrom(src => src.Document))
            .ForMember(dest => dest.Birthday, opts => opts.MapFrom(src => src.Birthday))
            .ForMember(dest => dest.Gender, opts => opts.MapFrom(src => src.Gender))
            .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status));

        CreateMap<DriverDto, DriverPublicApiDto>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.FullName, opts => opts.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Document, opts => opts.MapFrom(src => src.Document))
            .ForMember(dest => dest.Birthday, opts => opts.MapFrom(src => src.Birthday))
            .ForMember(dest => dest.Gender, opts => opts.MapFrom(src => src.Gender))
            .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status));

        CreateMap<Domain.Aggregates.Driver, CreateDriverCommandResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src));

        CreateMap<Domain.Aggregates.Driver, GetDriverByIdQueryResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src));

        CreateMap<DriverDto, GetDriverByIdQueryResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src));

        CreateMap<Page<DriverDto>, GetDriverPagedQueryResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src.Data))
            .ForMember(dest => dest.Pagination, opts => opts.MapFrom(src => src.PageInfo));

        // Query -> Filter
        CreateMap<GetDriverPagedQuery, DriverPagedFilter>()
            .ForMember(dest => dest.Cursor, opts => opts.MapFrom(src => src.Cursor))
            .ForMember(dest => dest.Pagination, opts => opts.MapFrom(src => src));
    }
}

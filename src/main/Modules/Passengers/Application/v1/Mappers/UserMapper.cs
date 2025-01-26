using JacksonVeroneze.NET.Pagination.Cursor;
using TaxiService.Modules.Passengers.Application.v1.Models.User;
using TaxiService.Modules.Passengers.Application.v1.Queries.User;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.Domain.Filters;

namespace TaxiService.Modules.Passengers.Application.v1.Mappers;

public class UserMapper : Profile
{
    public UserMapper()
    {
        // Entity -> Response
        CreateMap<UserEntity, UserResponse>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.FullName, opts => opts.MapFrom(src => src.Name!.Value))
            .ForMember(dest => dest.Document, opts => opts.MapFrom(src => src.Cpf!.Value))
            .ForMember(dest => dest.Birthday, opts => opts.MapFrom(src => src.Birthday))
            .ForMember(dest => dest.Gender, opts => opts.MapFrom(src => src.Gender))
            .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status));

        CreateMap<UserEntity, GetUserByIdQueryResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src));

        CreateMap<UserEntity, CreateUserCommandResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src));

        CreateMap<Page<UserEntity>, GetUserPagedQueryResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src.Data))
            .ForMember(dest => dest.Pagination, opts => opts.MapFrom(src => src.PageInfo));

        // Query -> Filter
        CreateMap<GetUserPagedQuery, UserPagedQuery>()
            .ForMember(dest => dest.Filter, opts => opts.Ignore())
            .ForMember(dest => dest.Pagination, opts => opts.MapFrom(src => src));
    }
}
using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Queries.Base;

namespace JacksonVeroneze.TemplateWebApi.Application.Mappers;

public class CommonMapper : Profile
{
    public CommonMapper()
    {
        CreateMap<PagedQuery, PaginationParameters>()
            .ConstructUsing(src =>
                new PaginationParameters(src.Page!.Value,
                    src.PageSize!.Value, src.OrderBy, src.Order));

        CreateMap<PageInfo, PageInfoResponse>()
            .ForMember(dest => dest.Page, opts => opts.MapFrom(src => src.Page))
            .ForMember(dest => dest.PageSize, opts => opts.MapFrom(src => src.PageSize))
            .ForMember(dest => dest.TotalPages, opts => opts.MapFrom(src => src.TotalPages))
            .ForMember(dest => dest.TotalElements, opts => opts.MapFrom(src => src.TotalElements))
            .ForMember(dest => dest.IsFirstPage, opts => opts.MapFrom(src => src.IsFirstPage))
            .ForMember(dest => dest.IsLastPage, opts => opts.MapFrom(src => src.IsLastPage))
            .ForMember(dest => dest.HasNextPage, opts => opts.MapFrom(src => src.HasNextPage))
            .ForMember(dest => dest.HasBackPage, opts => opts.MapFrom(src => src.HasBackPage))
            .ForMember(dest => dest.NextPage, opts => opts.MapFrom(src => src.NextPage))
            .ForMember(dest => dest.BackPage, opts => opts.MapFrom(src => src.BackPage));
    }
}

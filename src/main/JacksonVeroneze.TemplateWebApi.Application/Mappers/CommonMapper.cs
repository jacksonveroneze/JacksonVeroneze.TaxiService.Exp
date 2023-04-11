using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Request.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response.Pagination;

namespace JacksonVeroneze.TemplateWebApi.Application.Mappers;

public class CommonMapper : Profile
{
    public CommonMapper()
    {
        CreateMap<PagedRequest, PaginationParameters>()
            .ConstructUsing(src =>
                new PaginationParameters(src.Page!.Value,
                    src.PageSize!.Value, src.OrderBy, src.Order));

        CreateMap<PageInfo, PageInfoResponse>()
            .ForMember(dest => dest.Page, opts => opts.MapFrom(src => src.Page))
            .ForMember(dest => dest.PageSize, opts => opts.MapFrom(src => src.PageSize))
            .ForMember(dest => dest.TotalPages, opts => opts.MapFrom(src => src.TotalPages))
            .ForMember(dest => dest.TotalElements, opts => opts.MapFrom(src => src.TotalElements));
    }
}
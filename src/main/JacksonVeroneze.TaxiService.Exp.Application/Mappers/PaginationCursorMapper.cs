using JacksonVeroneze.NET.Pagination.Cursor;
using JacksonVeroneze.TaxiService.Exp.Application.Models.Base.Pagination.Cursor;
using JacksonVeroneze.TaxiService.Exp.Application.Queries.Base;

namespace JacksonVeroneze.TaxiService.Exp.Application.Mappers;

public class PaginationCursorMapper : Profile
{
    public PaginationCursorMapper()
    {
        CreateMap<PagedCursorQuery, PaginationParameters>()
            .ConstructUsing(src =>
                new PaginationParameters(src.PageSize!.Value,
                    src.Cursor, src.OrderBy, src.Order));

        CreateMap<PageInfo, PageInfoResponse>()
            .ForMember(dest => dest.HasMore, opts => opts.MapFrom(src => src.HasMore))
            .ForMember(dest => dest.Cursor, opts => opts.MapFrom(src => src.Cursor));
    }
}
using JacksonVeroneze.NET.Pagination.Cursor;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base.Pagination.CursorBased;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.Base;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Mappers;

public class PaginationCursorMapper : Profile
{
    public PaginationCursorMapper()
    {
        CreateMap<PagedCursorQuery, PaginationParameters>()
            .ConstructUsing(src =>
                new PaginationParameters(src.PageSize!.Value,
                    src.Cursor, src.OrderBy, src.Order));

        CreateMap<PageInfo, PageInfoCursorResponse>()
            .ForMember(dest => dest.HasMore, opts => opts.MapFrom(src => src.HasMore))
            .ForMember(dest => dest.Cursor, opts => opts.MapFrom(src => src.Cursor));
    }
}
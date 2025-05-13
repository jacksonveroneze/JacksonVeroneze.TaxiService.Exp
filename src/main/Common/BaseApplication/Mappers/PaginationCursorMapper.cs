using JacksonVeroneze.NET.Pagination.Cursor;

using TaxiService.Common.BaseApplication.Models.Base.Pagination.Cursor;
using TaxiService.Common.BaseApplication.Queries.Base;

namespace TaxiService.Common.BaseApplication.Mappers;

public class PaginationCursorMapper : Profile
{
    public PaginationCursorMapper()
    {
        CreateMap<PagedCursorQuery, PaginationParameters>()
            .ConstructUsing(src =>
                new PaginationParameters(
                    src.PageSize!.Value,
                    src.Cursor,
                    src.PageDirection,
                    src.OrderBy, src.OrderDirection));

        CreateMap<PageInfo, PageInfoResponse>()
            .ForMember(dest => dest.HasMore, opts => opts.MapFrom(src => src.HasMore))
            .ForMember(dest => dest.NextCursor, opts => opts.MapFrom(src => src.NextCursor))
            .ForMember(dest => dest.PreviousCursor, opts => opts.MapFrom(src => src.PreviousCursor));
    }
}

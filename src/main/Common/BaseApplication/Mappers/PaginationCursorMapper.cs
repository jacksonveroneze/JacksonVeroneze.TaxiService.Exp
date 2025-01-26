using JacksonVeroneze.NET.Pagination.Cursor;
using TaxiService.Common.Application.Models.Base.Pagination.Cursor;
using TaxiService.Common.Application.Queries.Base;

namespace TaxiService.Common.Application.Mappers;

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
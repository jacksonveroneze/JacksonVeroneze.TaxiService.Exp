using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response.Pagination;

namespace JacksonVeroneze.TemplateWebApi.Application.Models.Bank;

public record GetBankPagedQueryResponse : PagedResponse<BankResponse>
{
}

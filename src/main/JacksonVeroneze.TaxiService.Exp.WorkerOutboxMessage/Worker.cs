using System.Text.Json;
using JacksonVeroneze.TaxiService.Exp.Application.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.Interfaces.Repositories.Outbox;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using Quartz;

namespace JacksonVeroneze.TaxiService.Exp.WorkerOutboxMessage;

public class Worker(
    ILogger<Worker> logger,
    IOutboxWriteRepository outboxWriteRepository) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        ICollection<OutboxMessageEntity> messages =
            await outboxWriteRepository
                .GetAllAsync(CancellationToken.None);

        if (!messages.Any())
        {
            return;
        }

        foreach (OutboxMessageEntity message in messages)
        {
            try
            {
                OutboxMessageEntity? mess = await outboxWriteRepository
                    .GetByIdAsync(message!.Id!.Value, CancellationToken.None);

                OutBoxEmail? content = JsonSerializer
                    .Deserialize<OutBoxEmail>(mess!.Content!);

                await SendMailAsync(mess.Content!, content!.Email);

                await outboxWriteRepository.UpdateAsync(
                    mess, CancellationToken.None);
            }
            catch (Exception e)
            {
                logger.LogGenericError(nameof(Worker), nameof(Execute), e.Message);
            }
        }
    }

    private static Task SendMailAsync(string content, string dest)
    {
        Console.WriteLine($"Mail send to: {dest} - {content}");

        return Task.CompletedTask;
    }

    public record OutBoxEmail(Guid UserId, string Email);
}
using JacksonVeroneze.TaxiService.Exp.Infrastructure.Configurations;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Extensions;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.Extensions;
using JacksonVeroneze.TaxiService.Exp.WorkerOutboxMessage;
using Quartz;

HostApplicationBuilder builder =
    Host.CreateApplicationBuilder(args);

builder.Services.AddQuartz(cfg =>
{
    JobKey jobKey = new(nameof(Worker));

    cfg.AddJob<Worker>(jobKey).AddTrigger(t =>
        t.ForJob(jobKey).WithSimpleSchedule(s =>
            s.WithIntervalInSeconds(10)
                .RepeatForever())
    );
});

builder.Services.AddQuartzHostedService();

AppConfiguration appConfiguration =
    builder.Services.AddAppConfigs(builder.Configuration);

builder.Services
    .AddAppServices()
    .AddEfCoreServices()
    .AddDatabaseEfCore(appConfiguration);

IHost host = builder.Build();

await host.RunAsync();
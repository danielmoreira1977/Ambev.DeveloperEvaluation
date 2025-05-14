using Ambev.DeveloperEvaluation.Migrator;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ServiceDefaults;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddHostedService<Worker>();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

builder.Services.AddDbContext<DefaultContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ambev-db")
        ?? throw new InvalidOperationException("Connection string 'ambev-db' not found.")));

builder.Services.AddScoped<IDefaultContext>(provider => provider.GetRequiredService<DefaultContext>());

var host = builder.Build();
host.Run();

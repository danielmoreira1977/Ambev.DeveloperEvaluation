using Ambev.DeveloperEvaluation.Application;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.IoC;
using Ambev.DeveloperEvaluation.ServiceDefaults;
using Ambev.DeveloperEvaluation.WebApi.Endpoints;
using Ambev.DeveloperEvaluation.WebApi.Helpers;
using Ambev.DeveloperEvaluation.WebApi.Middleware;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddOpenApi();
builder.Services.AddHttpContextAccessor();

builder.RegisterDependencies();
builder.Services.AddScoped<IHttpContextHelper, HttpContextHelper>();

builder.Services.AddProblemDetails(options =>
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Instance = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";
        context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);

        var activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
        context.ProblemDetails.Extensions.TryAdd("traceId", activity?.TraceId.ToString() ?? string.Empty);
    }
    );

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(
        typeof(ApplicationLayer).Assembly,
        typeof(Program).Assembly
    );
});

var app = builder.Build();

app.UseMiddleware<ValidationExceptionMiddleware>();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

//Mapping endpoints
app.MapAuthEndpoints();
app.MapCartsEndpoints();
app.MapProductEndpoints();
app.MapUserEndpoints();

app.Run();

using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.WebApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddOpenApi();

var app = builder.Build();

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

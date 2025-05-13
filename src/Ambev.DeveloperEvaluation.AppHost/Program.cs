var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Ambev_DeveloperEvaluation_WebApi>("ambev-developerevaluation-webapi");

builder.Build().Run();

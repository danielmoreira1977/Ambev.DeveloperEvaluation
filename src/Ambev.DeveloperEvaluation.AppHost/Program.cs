var builder = DistributedApplication.CreateBuilder(args);

var username = builder.AddParameter("bus-username", secret: true);
var password = builder.AddParameter("bus-password", secret: true);

var rabbitmq = builder.AddRabbitMQ("ambev-bus-rabbitMQ", username, password)
    .WithManagementPlugin()
    .WithDataVolume(isReadOnly: false);

var dbUsername = builder.AddParameter("db-username", secret: true);
var dbPassword = builder.AddParameter("db-password", secret: true);

var postgres = builder.AddPostgres("ambev-db-server", dbUsername, dbPassword)
    .WithPgAdmin()
    .WithDataVolume(isReadOnly: false);

var database = postgres.AddDatabase("ambev-db");

var migrator = builder.AddProject<Projects.Ambev_DeveloperEvaluation_Migrator>("ambev-developerevaluation-migrator")
    .WithReference(database).WaitFor(database);

builder.AddProject<Projects.Ambev_DeveloperEvaluation_WebApi>("ambev-developerevaluation-webapi")
    .WithReference(rabbitmq).WaitFor(rabbitmq)
    .WithReference(database).WaitFor(database);

builder.Build().Run();

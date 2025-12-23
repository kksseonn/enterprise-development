var builder = DistributedApplication.CreateBuilder(args);

var password = builder.AddParameter("DatabasePassword");
var dbName = "library";

var libraryDb = builder
    .AddPostgres("library-db", password: password)
    .AddDatabase(dbName);

var natsStream = builder.AddParameter("StreamName");
var natsSubject = builder.AddParameter("SubjectName");
var natsConsumer = builder.AddParameter("ConsumerName");

var nats = builder.AddNats("nats-broker")
    .WithJetStream()
    .WithLifetime(ContainerLifetime.Persistent);

builder.AddContainer("nats-ui", "ghcr.io/nats-nui/nui")
    .WithReference(nats)
    .WaitFor(nats)
    .WithHttpEndpoint(port: 31311, targetPort: 31311);

var apiHost = builder.AddProject<Projects.Library_Api_Host>("library-api-host")
    .WithReference(libraryDb, "LibraryDb")
    .WaitFor(libraryDb)
    .WithReference(nats)
    .WaitFor(nats)
    .WithEnvironment("Nats__StreamName", natsStream)
    .WithEnvironment("Nats__SubjectName", natsSubject)
    .WithEnvironment("Nats__ConsumerName", natsConsumer); ;

builder.AddProject<Projects.Library_Generator_Nats_Host>("library-generator-nats-host")
    .WithReference(nats)
    .WaitFor(nats)
    .WithEnvironment("Nats__StreamName", natsStream)
    .WithEnvironment("Nats__SubjectName", natsSubject);

builder.Build().Run();
var builder = DistributedApplication.CreateBuilder(args);

var password = builder.AddParameter("DatabasePassword");
var dbName = "library";

var libraryDb = builder
    .AddPostgres("library-db", password: password)
    .AddDatabase(dbName);

var nats = builder
    .AddNats("nats-broker")
    .WithJetStream()
    .WithLifetime(ContainerLifetime.Persistent);

const string streamName = "LIBRARY_STREAM";
const string subjectName = "library.borrows.ingest";

builder.AddProject<Projects.Library_Api_Host>("library-api-host")
    .WithReference(libraryDb, "LibraryDb")
    .WaitFor(libraryDb)
    .WithReference(nats)
    .WithEnvironment("Nats__StreamName", streamName)
    .WithEnvironment("Nats__SubjectName", subjectName);

builder.AddContainer("nats-ui", "natsio/nats-ui")
    .WithHttpEndpoint(port: 8222, targetPort: 80)
    .WithReference(nats);

builder.AddProject<Projects.Library_Generator_Nats_Host>("library-generator-nats-host")
    .WithReference(nats)
    .WithEnvironment("Nats__StreamName", streamName)
    .WithEnvironment("Nats__SubjectName", subjectName);

builder.Build().Run();
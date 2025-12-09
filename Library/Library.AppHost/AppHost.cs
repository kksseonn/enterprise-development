var builder = DistributedApplication.CreateBuilder(args);

var password = builder.AddParameter("DatabasePassword");
var dbName = "library";

var libraryDb = builder
    .AddPostgres("library-db", password: password)
    .AddDatabase(dbName);

builder.AddProject<Projects.Library_Api_Host>("library-api-host")
    .WithReference(libraryDb, "Database")
    .WaitFor(libraryDb);

builder.Build().Run();
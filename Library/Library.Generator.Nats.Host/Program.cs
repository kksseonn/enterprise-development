using Library.Generator.Nats.Host;
using Library.Generator.Nats.Host.Services;
using Library.Infrastructure.Nats.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<NatsOptions>(
    builder.Configuration.GetSection(NatsOptions.SectionName));

builder.AddNatsClient("nats-broker");

builder.Services.AddSingleton<BorrowNatsProducer>();
builder.Services.AddScoped<IBorrowsGenerator, BorrowsGenerator>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var assemblies = AppDomain.CurrentDomain.GetAssemblies()
        .Where(a => a.GetName().Name!.StartsWith("Library"))
        .Distinct();

    foreach (var assembly in assemblies)
    {
        var xmlFile = $"{assembly.GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

        if (File.Exists(xmlPath))
        {
            options.IncludeXmlComments(xmlPath);
        }
    }
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
using Library.Generator.Nats.Host.Services;
using Library.Infrastructure.Nats;
using Library.Infrastructure.Nats.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<NatsOptions>(
    builder.Configuration.GetSection(NatsOptions.SectionName));

builder.AddNatsClient("nats-broker");

builder.Services.AddSingleton<LibraryJetStreamProducer>();

builder.Services.AddScoped<IBorrowsGenerator, BorrowsGenerator>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

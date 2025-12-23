using Library.Application.Contracts;
using Library.Application.Contracts.Book;
using Library.Application.Contracts.Borrow;
using Library.Application.Contracts.EditionType;
using Library.Application.Contracts.Publisher;
using Library.Application.Contracts.Reader;
using Library.Application.Mapper;
using Library.Application.Service;
using Library.Domain;
using Library.Domain.Entities;
using Library.Infrastructure;
using Library.Infrastructure.Nats.Options;
using Library.Infrastructure.Repository;
using Library.ServiceDefaults;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

var config = TypeAdapterConfig.GlobalSettings;
config.Scan(typeof(MappingRegister).Assembly);

builder.Services.AddSingleton(config);
builder.Services.AddScoped<IMapper, ServiceMapper>();

builder.Services.AddScoped<IRepository<EditionType>, EditionTypeRepository>();
builder.Services.AddScoped<IRepository<Publisher>, PublisherRepository>();
builder.Services.AddScoped<IRepository<Reader>, ReaderRepository>();
builder.Services.AddScoped<IRepository<Book>, BookRepository>();
builder.Services.AddScoped<IRepository<Borrow>, BorrowRepository>();

builder.Services.AddScoped<IEditionTypeCrudService, EditionTypeService>();
builder.Services.AddScoped<IPublisherCrudService, PublisherService>();
builder.Services.AddScoped<IReaderCrudService, ReaderService>();
builder.Services.AddScoped<IBookCrudService, BookService>();
builder.Services.AddScoped<IBorrowCrudService, BorrowService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

builder.Services.AddHostedService<BorrowNatsConsumer>();

builder.Services.Configure<NatsOptions>(
    builder.Configuration.GetSection(NatsOptions.SectionName));
builder.AddNatsClient("nats-broker");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var assemblies = AppDomain.CurrentDomain.GetAssemblies()
        .Where(a => a.GetName().Name!.StartsWith("Library"))
        .Distinct();

    foreach (var assembly in assemblies)
    {
        var xmlFile = $"{assembly.GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        if (File.Exists(xmlPath))
            c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
    }
});

builder.AddNpgsqlDbContext<LibraryDbContext>(
    "LibraryDb",
    configureDbContextOptions: builder => builder.UseLazyLoadingProxies());

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
    await context.Database.MigrateAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapDefaultEndpoints();

app.Run();
using Library.Application.Contracts.EditionType;
using Library.Application.Mapper;
using Library.Application.Service;
using Library.Domain.Data;
using Library.Domain.Entities;
using Library.Domain.IRepository;
using Library.Infrastructure;
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

builder.Services.AddSingleton<LibraryData>();

builder.Services.AddScoped<IEditionTypeRepository, EditionTypeRepository>();

builder.Services.AddScoped<IEditionTypeReadService, EditionTypeService>();

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
            c.IncludeXmlComments(xmlPath);
    }
});

builder.AddNpgsqlDbContext<LibraryDbContext>(
    "Database",
    configureDbContextOptions: options => options.UseLazyLoadingProxies()
);

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

using Core.Contracts;
using Core.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.UnitOfWork;
using Persistence.UnitOfWork.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services
    // we have to specify that its supposed to substitute the DBContext, else it has an ambiguity issue
    .AddDbContext<DbContext, ApplicationDbContext>(options => options.UseSqlServer(connectionString)) 
    .AddScoped<IUnitOfWork, UnitOfWork>()
    .AddTransient<IEquipmentRepository, EquipmentRepository>()
    .AddTransient<ILocationRepository, LocationRepository>()
    .AddTransient<IObservationRepository, ObservationRepository>()
    .AddTransient<IResearchProjectRepository, ResearchProjectRepository>()
    .AddTransient<ISpeciesRepository, SpeciesRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(b => b.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
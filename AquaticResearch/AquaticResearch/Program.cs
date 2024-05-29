using AquaticResearch.Model.Entities;
using AquaticResearch.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register the DbContext with the dependency injection container
builder.Services.AddDbContext<AquaticDbContext>(options =>
    options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=AquaticResearch;ConnectRetryCount=0"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AquaticDbContext>();
    
    if (!context.Equipments.Any())
    {
        context.Equipments.Add(new Equipment { Name = "Basic Scuba Gear", Description = "13x34"});
        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

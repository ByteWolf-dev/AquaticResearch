using System.Text;
using Core.Contracts;
using Core.Contracts.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence;
using Persistence.UnitOfWork;
using Persistence.UnitOfWork.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "JWTDemo", Version = "v1" });

    var xmlFile = "JwtDemo.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    Console.WriteLine(xmlPath);
        
    c.IncludeXmlComments(xmlPath);

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name         = "Authorization",
        Type         = SecuritySchemeType.ApiKey,
        Scheme       = "Bearer",
        BearerFormat = "JWT",
        In           = ParameterLocation.Header,
        Description  = "JWT Authorization header using the Bearer scheme. \r\n\r\n" +
                       "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
                       "Example: \"Bearer 1safsfsdfdfd\""
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id   = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer           = true,
            ValidateAudience         = true,
            ValidateLifetime         = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer              = builder.Configuration["Jwt:Issuer"],
            ValidAudience            = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey         = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization(options =>
{
    //options.AddPolicy("Admin", policy => policy.RequireClaim("IsAdmin"));
});

builder.Services.AddTransient<UserManager>();

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
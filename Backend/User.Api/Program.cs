using Common;
using Common.Interfaces;
using Common.Profiles;
using Common.Utilities;
using Common.Validations;
using Data;
using Data.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var config = builder.Configuration;

builder.Services.AddJwtAuthentication(config);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    //options.UseLazyLoadingProxies();
    options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
});

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddMediatR(c => 
    c.RegisterServicesFromAssemblyContaining<Program>()
);
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


builder.Services.AddAutoMapper(typeof(AutomapperProfiles).Assembly);


//Regester repos
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IExceptionHandler, ExceptionHandler>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

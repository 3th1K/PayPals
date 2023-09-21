using Common.Interfaces;
using Common.Utilities;
using Common.Validations;
using Data.Models;
using FluentValidation;
using GroupService.Api.Interfaces;
using GroupService.Api.Models;
using GroupService.Api.Queries;
using GroupService.Api.Repositories;
using GroupService.Api.Validations;
using MediatR;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var config = builder.Configuration;

builder.Services.AddJwtAuthentication(config);

builder.Services.AddAuthorization();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
});

builder.Services.AddValidatorsFromAssemblyContaining<Program>(ServiceLifetime.Scoped);

builder.Services.AddMediatR(c =>
    c.RegisterServicesFromAssemblyContaining<Program>()
    //.AddBehavior<IPipelineBehavior<GetGroupByIdQuery, GroupResponse>, ValidationBehavior<GetGroupByIdQuery, GroupResponse>>()
    //.AddBehavior<IPipelineBehavior<GroupRequest, GroupResponse>, ValidationBehavior<GroupRequest, GroupResponse>>()
);
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddScoped<IGroupRepository, GroupRepository>();
//builder.Services.AddScoped<IErrorBuilder, ErrorBuilder>();
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

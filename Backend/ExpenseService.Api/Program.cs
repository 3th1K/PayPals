using Common.Interfaces;
using Common.Utilities;
using ExpenseService.Api.Interfaces;
using ExpenseService.Api.Models;
using ExpenseService.Api.Queries;
using ExpenseService.Api.Repositories;
using ExpenseService.Api.Validations;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
    .AddBehavior<IPipelineBehavior<ExpenseRequest, ExpenseResponse>, ValidationBehavior<ExpenseRequest, ExpenseResponse>>()
    .AddBehavior<IPipelineBehavior<GetExpenseDetailsByIdQuery, Expense>, ValidationBehavior<GetExpenseDetailsByIdQuery, Expense>>()
);


builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
builder.Services.AddScoped<IErrorBuilder, ErrorBuilder>();

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

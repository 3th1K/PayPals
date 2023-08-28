using FluentValidation;
using LanguageExt.Common;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UserService.Api.Interfaces;
using UserService.Api.Models;
using UserService.Api.Queries;
using UserService.Api.Repositories;
using UserService.Api.Validations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var config = builder.Configuration;

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = config["JwtSettings:issuer"],
        ValidAudience = config["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddValidatorsFromAssemblyContaining<Program>(ServiceLifetime.Scoped);

builder.Services.AddMediatR(c => 
    c.RegisterServicesFromAssemblyContaining<Program>()
    .AddBehavior<IPipelineBehavior<UserRequest, Result<UserResponse>>, ValidationBehavior<UserRequest, UserResponse>>()
    .AddBehavior<IPipelineBehavior<UserUpdateRequest, Result<UserResponse>>, ValidationBehavior<UserUpdateRequest, UserResponse>>()
    .AddBehavior<IPipelineBehavior<GetUserByIdQuery, Result<UserResponse>>, ValidationBehavior<GetUserByIdQuery, UserResponse>>()
    //.AddBehavior<IPipelineBehavior<GetUserGroupsQuery, List<Group>>, ValidationBehavior<GetUserGroupsQuery, List<Group>>>()
    .AddBehavior<IPipelineBehavior<GetUserDetailsByIdQuery, Result<User>>, ValidationBehavior<GetUserDetailsByIdQuery, User>>()
    .AddBehavior<IPipelineBehavior<DeleteUserQuery, Result<User>>, ValidationBehavior<DeleteUserQuery, User>>()
);


builder.Services.AddAutoMapper(typeof(Program).Assembly);


//Regester repos
builder.Services.AddScoped<IUserRepository, UserRepository>();



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

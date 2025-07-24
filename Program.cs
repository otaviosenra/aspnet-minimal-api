using MinimalApi.Infra.Db;
using MinimalApi.Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Domain.Services;
using MinimalApi.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DbContexto>(options => {
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("sqlserver")
    );
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Hello World!");

app.MapPost("/login",  ([FromBody] LoginDTO loginDTO, IUserService userService) =>
{
    if (userService.Login(loginDTO))
        return Results.Ok("Login successful");
    else
        return Results.Unauthorized();
});

app.Run();


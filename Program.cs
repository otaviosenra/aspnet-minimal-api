using MinimalApi.Infra.Db;
using MinimalApi.Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Domain.Services;
using MinimalApi.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ILoginService, LoginService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddDbContext<DbContexto>(options => {
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("sqlserver")
    );
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.MapGet("/", () => app.MapSwagger());

// app.MapPost("/login",  );

app.Run();


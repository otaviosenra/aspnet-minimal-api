using MinimalApi.Infra.Db;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Domain.Services;
using MinimalApi.Domain.Interfaces;


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
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sua API V1");
    c.RoutePrefix = string.Empty; // Faz com que o swagger abra na rota raiz
});

app.MapControllers();

app.Run();


using MinimalApi.Infra.Db;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Domain.Services;
using MinimalApi.Domain.Interfaces;
using MinimalApi.Domain.Utils;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ICheeseService, CheeseService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddEndpointsApiExplorer();

// builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options.UseInlineDefinitionsForEnums(); 
    options.SchemaFilter<EnumSchemaFilter>(); 
});



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


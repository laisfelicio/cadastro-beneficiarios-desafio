using Desafio_Tecnico_Cadastro_de_Beneficiarios.Services;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Data;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Profiles;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Services.Interface;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

Env.Load();
/*builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("4tech-db-memory");
});*/

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPlanoInterface, PlanoService>();
builder.Services.AddScoped<IBeneficiarioInterface, BeneficiarioService>();

builder.Services.AddAutoMapper(typeof(PlanoProfile).Assembly);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();

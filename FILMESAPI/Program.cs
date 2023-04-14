using FILMESAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
#region Banco de Dados
var connectionString = builder.Configuration.GetConnectionString("FilmeConnection"); // Variavel que será responsavel por determinar o caminho do banco

builder.Services.AddDbContext<FilmeContext>(options =>
    options.UseLazyLoadingProxies().UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
#endregion


#region Configuração para utilizar o  AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


#endregion
// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(); // Para manipular dados via Json  precisamos de um biblioteca Microsoft.AspNetCore.Mvc.NewtonsoftJson e add ao controllador 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>

   {
       c.SwaggerDoc("v1", new OpenApiInfo { Title = "FilmesAPI", Version = "v1" });
       var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
       var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
       c.IncludeXmlComments(xmlPath);

       //c.SwaggerDoc("v1", new OpenApiInfo { Title = "FilmesAPI", Version = "v1" });
       //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
       //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
       //c.IncludeXmlComments(xmlPath);

   });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

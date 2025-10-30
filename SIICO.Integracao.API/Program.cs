using SIICO.Aplicacao.Interfaces;
using SIICO.Aplicacao.Services;
using SIICO.Dominio.Interfaces;
using SIICO.Infraestrutura.Repositorio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection - Domain
builder.Services.AddScoped<ICorrespondenteConvenioRepository, CorrespondenteConvenioRepository>();

// Dependency Injection - Application
builder.Services.AddScoped<IHateoasService, HateoasService>();
builder.Services.AddScoped<ICorrespondenteConvenioService, CorrespondenteConvenioService>();

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

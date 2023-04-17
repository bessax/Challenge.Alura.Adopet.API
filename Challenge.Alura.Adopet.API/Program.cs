using Challenge.Alura.AdoAbrigo.API.Service;
using Challenge.Alura.AdoEndereco.API.Service;
using Challenge.Alura.Adopet.API.Data;
using Challenge.Alura.Adopet.API.Dominio;
using Challenge.Alura.Adopet.API.Repository;
using Challenge.Alura.Adopet.API.Repository.Interface;
using Challenge.Alura.Adopet.API.Service;
using Challenge.Alura.Adopet.API.Service.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AdoPetContext>(options =>
{
    options.UseSqlServer("Name=AdopetAPI");
});

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<AdoPetContext>();
//Repositorys
builder.Services.AddScoped<IRepository<Tutor>,TutorRepository>();
builder.Services.AddScoped<IRepository<Pet>, PetRepository>();
builder.Services.AddScoped<IRepository<Abrigo>, AbrigoRepository>();
builder.Services.AddScoped<IRepository<Endereco>, EnderecoRepository>();

//Services
builder.Services.AddScoped<ITutorService,TutorService>();
builder.Services.AddScoped<IPetService, PetService>();
builder.Services.AddScoped<IAbrigoService, AbrigoService>();
builder.Services.AddScoped<IEnderecoService, EnderecoService>();

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

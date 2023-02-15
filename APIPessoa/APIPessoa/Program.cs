using APIPessoa.Filter;
using APIPessoa.Infra.Data.Repository;
using APIPessoa.Service;
using APIPessoa.Service.Dto;
using APIPessoa.Service.Entity;
using APIPessoa.Service.Interface;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
//{
//    options.SuppressModelStateInvalidFilter = true;
//});

builder.Services.AddMvc(options =>
{
    options.Filters.Add(typeof(ExcecaoGeralFilter));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();
builder.Services.AddScoped<IPessoaService, PessoaService>();
builder.Services.AddScoped<IGerarTokenService, GerarTokenService>();

MapperConfiguration mapperConfig = new(mc =>
{
    mc.CreateMap<PessoaEntity, PessoaDto>().ReverseMap();
});

IMapper mapper = mapperConfig.CreateMapper();

builder.Services.AddSingleton(mapper);

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

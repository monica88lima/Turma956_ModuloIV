using APIProdutos.Core.Interface;
using APIProdutos.Core.Service;
using APIProdutos.Filters;
using APIProdutos.Infra.Data.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("PolicyCors",
        policy =>
        {
            policy.WithOrigins("https://localhost:7179")
                    .WithHeaders("x-teste")
                    .WithMethods("GET", "POST");

            //policy.AllowAnyOrigin();
            //policy.AllowAnyHeader();
            //policy.AllowAnyMethod();
        });
});

// Add services to the container.
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var key = Encoding.ASCII.GetBytes(builder.Configuration["secretKey"]);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) //Adiciono o esquema de JWT Bearer
    .AddJwtBearer(options =>
    {
        //Adiciona as opções de validação
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true, // para inativar a validação do issuer, informar false e remover ValidIssuer
            ValidateAudience = true, // para inativar a validação da audience, informar false e remover ValidAudience
            ValidIssuer = "APIClientes.com",
            ValidAudience = "APIProdutos.com"
        };
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMvc(options =>
{
    options.Filters.Add<LogResultFilter>();
    options.Filters.Add<GeneralExceptionFilter>();
});

builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<GaranteProdutoExisteActionFilter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("PolicyCors");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

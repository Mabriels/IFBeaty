using IFBeaty.Data;
using IFBeaty.Repositorios;
using IFBeaty.Services;
using Microsoft.EntityFrameworkCore;
//imports feitos para a parte de autenticacao
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

// var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddCors(options =>
// {
//   options.AddPolicy(name: MyAllowSpecificOrigins,
//                     policy =>
//                     {
//                       policy.WithOrigins("http://localhost:5173"); // add the allowed origins  
//                     });
// });

builder.Services.AddCors(options =>
{
  options.AddDefaultPolicy(
      policy =>
      {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyMethod()
            .AllowAnyHeader();
      });
});

// builder.Services.AddCors(options =>
// {
//     options.AddDefaultPolicy(
//         policy =>
//         {
//             policy.WithOrigins("http://example.com",
//                                 "http://www.contoso.com");
//         });
// });

//Regras que tem que adicionar
//Não pode excluir procedimento que esteja vinculado a uma agendamento já (precisa do agendamento funcionando)
//Nao pode excluir dia funcionamento com agendamento
//Tem que pensar a regras de excluir agendamento
//Colocar validacao e mensagens de erro nos dtos novos
// Add services to the container.

//Adicionando a minha classe de contexto na API
//Tem que acrescentar using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;
builder.Services.AddDbContext<ContextoBD>(
    options =>
    //Dizendo que vamos usar o MySQL
    options.UseMySql(
        //Pegando as configurações de acesso ao BD
        builder.Configuration.GetConnectionString("ConexaoBanco"),
        //Detectando o Servidor de BD
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("ConexaoBanco"))
    )
);

//Configurações para usar Autenticação com JWT
var JWTChave = Encoding.ASCII.GetBytes(builder.Configuration["JWTChave"]);
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
      options.SaveToken = true;
      options.TokenValidationParameters = new TokenValidationParameters
      {
        IssuerSigningKey = new SymmetricSecurityKey(JWTChave),
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
      };
    });

//Classes que serao adicionadas no Container de Injecao de Dependência
builder.Services.AddScoped<ProcedimentoServico>();
builder.Services.AddScoped<ProcedimentoRepositorio>();
builder.Services.AddScoped<UsuarioServico>();
builder.Services.AddScoped<UsuarioRepositorio>();
builder.Services.AddScoped<DiaFuncionamentoRepositorio>();
builder.Services.AddScoped<DiaFuncionamentoServico>();
builder.Services.AddScoped<AgendamentoServico>();
builder.Services.AddScoped<AgendamentoRepositorio>();
builder.Services.AddScoped<PerfilRepositorio>();
builder.Services.AddScoped<AutenticacaoServico>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

//Desabilitando o CORS para o endereco do Front
//Coloque o endereco do seu front
// app.UseCors(policy =>
//     policy.WithOrigins("http://127.0.0.1:5173")
//     .AllowAnyMethod()
//     .AllowAnyHeader()
// );



// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//   app.UseSwagger();
//   app.UseSwaggerUI();
// }

app.UseCors();
// app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


//Atualizacao pro 7
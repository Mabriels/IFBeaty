using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using IFBeaty.Dtos.Usuario;
using IFBeaty.Models;
using IFBeaty.Repositorios;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IFBeaty.Services;

public class AutenticacaoServico
{
  private readonly UsuarioRepositorio _usuarioRepositorio;
  private readonly IConfiguration _configuration;

  public AutenticacaoServico([FromServices] UsuarioRepositorio repositorio,
    [FromServices] IConfiguration configuration)
  {
    _usuarioRepositorio = repositorio;
    _configuration = configuration;
  }

  public string Login(UsuarioLoginRequisicao usuarioLogin)
  {

    //Buscar o usuario com aquele email no BD
    var usuario = _usuarioRepositorio.BuscarUsuarioPeloEmail(usuarioLogin.Email);

    //Usuario nao encontrado ou senha não bate
    if ((usuario is null) || (!BCrypt.Net.BCrypt.Verify(usuarioLogin.Senha, usuario.Senha)))
    {
      throw new Exception("Usuário ou senha incorretos");
    }

    //Gerar o JWT (vou colocar daqui a pouco)
    var tokenJWT = GerarJWT(usuario);

    return tokenJWT;
  }

  private string GerarJWT(Usuario usuario)
  {
    //Pegando a chave JWT
    var JWTChave = Encoding.ASCII.GetBytes(_configuration["JWTChave"]);

    //Criando as credenciais
    var credenciais = new SigningCredentials(
            new SymmetricSecurityKey(JWTChave),
            SecurityAlgorithms.HmacSha256);

    // var claims = new Claim[]
    // {
    //   //Nome do usuario
    //   new Claim(ClaimTypes.Name, usuario.Nome),

    //   // new Claim(ClaimTypes.Role, "adm"),
    //   // new Claim(ClaimTypes.Role, "teste"),
    // };

    // foreach (var x in usuario.Perfis)
    // {
    //   claims.Append(new Claim(ClaimTypes.Role, x.Nome));
    // }

    //CLAIMS, informacoes sobre o usuario ou sobre o token
    var claims = new List<Claim>();



    //Nome do usuario
    claims.Add(new Claim(ClaimTypes.Name, usuario.Nome));
    claims.Add(new Claim("fruta", "banana"));

    //Id do Usario
    claims.Add(new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()));

    //Perfis do usuario
    foreach (var perfil in usuario.Perfis)
    {
      claims.Add(new Claim(ClaimTypes.Role, perfil.Nome));
    }



    //Criando o token
    var tokenJWT = new JwtSecurityToken(
        expires: DateTime.Now.AddHours(8),
        signingCredentials: credenciais,
        claims: claims
    );

    //Escrevendo o token e retornando
    return new JwtSecurityTokenHandler().WriteToken(tokenJWT);
  }
}

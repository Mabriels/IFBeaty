using IFBeaty.Dtos.Usuario;
using IFBeaty.Excecoes;
using IFBeaty.Models;
using IFBeaty.Repositorios;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace IFBeaty.Services;

public class UsuarioServico
{
  //Repositorio injetado no construtor
  private readonly UsuarioRepositorio _usuarioRepositorio;
  private readonly PerfilRepositorio _perfilRepositorio;

  //Construtor com injecao do repositorio
  public UsuarioServico([FromServices] UsuarioRepositorio repositorio, [FromServices] PerfilRepositorio pRepositorio)
  {
    _usuarioRepositorio = repositorio;
    _perfilRepositorio = pRepositorio;
  }

  public UsuarioResposta CriarUsuario(UsuarioCriarRequisicao novoUsuario)
  {

    //Verificar se existe um usuario com mesmo email
    var usuarioExiste = _usuarioRepositorio.BuscarUsuarioPeloEmail(novoUsuario.Email);

    if (usuarioExiste is not null)
    {
      throw new EmailExistenteException();
    }

    //Copiar dos dados da Requisicao para Modelo
    var usuario = novoUsuario.Adapt<Usuario>();

    //Regras específicas
    usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);

    //Mandando o repositorio salavar o modelo no BD
    usuario = _usuarioRepositorio.CriarUsuario(usuario);

    //Copiando os dados do Modelo para a Resposta
    var usuarioResposta = usuario.Adapt<UsuarioResposta>();

    //Retornando a resposta pro controlador
    return usuarioResposta;
  }

  public List<UsuarioResposta> ListarUsuarios()
  {
    //pegando todos (modelos)
    var usuarios = _usuarioRepositorio.ListarUsuarios();

    //copiar de modelo para resposta
    var usuarioRespostas = usuarios.Adapt<List<UsuarioResposta>>();

    return usuarioRespostas;
  }

  private Usuario BuscarPeloId(int id, bool tracking = true)
  {
    var usuario = _usuarioRepositorio.BuscarUsuarioPeloId(id, tracking);

    if (usuario is null)
    {
      throw new EmailExistenteException();
    }

    return usuario;
  }

  public UsuarioResposta BuscarUsuarioPeloId(int id)
  {
    //Buscar o modelo pelo ID
    var usuario = BuscarPeloId(id, false);

    //Copiando modelo para resposta
    var usuarioResposta = usuario.Adapt<UsuarioResposta>();

    //Retornndo pro controlador
    return usuarioResposta;
  }

  public void RemoverUsuario(int id)
  {
    //Se tiver alguma regra de negócio de exclusao colocar antes

    //Buscando o usuario que deseja remover pelo id
    var usuario = BuscarPeloId(id);

    //Mandando o repositorio remover
    _usuarioRepositorio.RemoverUsuario(usuario);

  }

  public UsuarioResposta AtualizarUsuario(int id, UsuarioAtualizarRequisicao usuarioEditado)
  {
    //Buscar o usuario (modelo) no repositório pelo id para poder editá-lo
    var usuario = BuscarPeloId(id);

    //Regras específicas
    if (usuario.Email != usuarioEditado.Email)
    {
      //Esta mudando o email e precisa verificar se ja nao existe usuario com esse email
      var usuarioExiste = _usuarioRepositorio.BuscarUsuarioPeloEmail(usuarioEditado.Email);

      if (usuarioExiste is not null)
      {
        throw new EmailExistenteException();
      }
    }

    //Copiar dos dados da Requisicao para Modelo
    usuarioEditado.Adapt(usuario);

    //Mandando o repositorio salvar as alteracoes no BD
    _usuarioRepositorio.AtualizarUsuario();

    //Copiando os dados do Modelo para a Resposta
    var usuarioResposta = usuario.Adapt<UsuarioResposta>();

    //Retornando a resposta pro controlador
    return usuarioResposta;
  }

  public UsuarioResposta AtribuirPerfil(int usuarioId, int perfilId)
  {
    //Buscar o usuario com os perfis associados
    var usuario = BuscarPeloId(usuarioId);

    //Buscar o perfil
    var perfil = _perfilRepositorio.BuscarPerfilPeloId(perfilId);
    if (perfil is null)
    {
      throw new Exception("Perfil não encontrado!");
    }

    //Verificar se o perfil já não está inserido no usuario
    if (usuario.Perfis.Exists(perfil => perfil.Id == perfilId))
    {
      throw new BadHttpRequestException("Perfil já foi adicionado anteriormente ao usuário");
    }

    //Associar o usuario com o perfil
    usuario.Perfis.Add(perfil);

    //Mandar o repositorio atualizar o usuario
    _usuarioRepositorio.AtualizarUsuario();

    //Copiar do modelo pra resposta e retornar
    return usuario.Adapt<UsuarioResposta>();
  }

  public UsuarioResposta RemoverPerfil(int usuarioId, int perfilId)
  {
    //Buscar o usuario com os perfis associados
    var usuario = BuscarPeloId(usuarioId);

    //Verificar se o perfil já não está removido do usuario
    if (!usuario.Perfis.Exists(perfil => perfil.Id == perfilId))
    {
      throw new BadHttpRequestException("Perfil já foi removido anteriormente do usuário");
    }

    //Associar o usuario com o perfil
    usuario.Perfis.RemoveAll(perfil => perfil.Id == perfilId);

    //Mandar o repositorio atualizar o usuario
    _usuarioRepositorio.AtualizarUsuario();

    //Copiar do modelo pra resposta e retornar
    return usuario.Adapt<UsuarioResposta>();

  }

}

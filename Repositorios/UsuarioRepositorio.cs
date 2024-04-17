using IFBeaty.Data;
using IFBeaty.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IFBeaty.Repositorios;

public class UsuarioRepositorio
{
  //Contexto injetado no construtor
  private readonly ContextoBD _contexto;

  //Construtor injetando o contexto
  public UsuarioRepositorio([FromServices] ContextoBD contexto)
  {
    _contexto = contexto;
  }

  public Usuario CriarUsuario(Usuario usuario)
  {
    //Mandar salvar o usuario no contexto
    _contexto.Usuarios.Add(usuario);
    _contexto.SaveChanges();

    return usuario;
  }

  public Usuario BuscarUsuarioPeloEmail(string email)
  {
    return _contexto.Usuarios
      .Include(usuario => usuario.Perfis)
      .AsNoTracking()
      .FirstOrDefault(usuario => usuario.Email == email);
  }

  public List<Usuario> ListarUsuarios()
  {
    return _contexto.Usuarios
      .Include(usuario => usuario.Endereco)
      .Include(usuario => usuario.Perfis)
      .AsNoTracking()
      .ToList();
  }

  public Usuario BuscarUsuarioPeloId(int id, bool tracking = true)
  {

    return tracking ?
      _contexto.Usuarios.Include(usuario => usuario.Endereco)
        .Include(u => u.Perfis).FirstOrDefault(usuario => usuario.Id == id) :
      _contexto.Usuarios.Include(usuario => usuario.Endereco)
        .Include(u => u.Perfis).AsNoTracking().FirstOrDefault(usuario => usuario.Id == id);

  }

  public void RemoverUsuario(Usuario usuario)
  {
    //Removendo do contexto
    _contexto.Remove(usuario);

    //Salvando a deleção no BD
    _contexto.SaveChanges();
  }

  public void AtualizarUsuario()
  {
    //Mandar salvar as mudanças no BD
    _contexto.SaveChanges();
  }
}




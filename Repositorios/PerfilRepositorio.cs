using IFBeaty.Data;
using IFBeaty.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IFBeaty.Repositorios;

public class PerfilRepositorio
{
  private readonly ContextoBD _contexto;

  public PerfilRepositorio([FromServices] ContextoBD contexto)
  {
    _contexto = contexto;
  }

  public Perfil BuscarPerfilPeloId(int id)
  {
    return _contexto.Perfis.AsNoTracking().FirstOrDefault(perfil => perfil.Id == id);
  }

}

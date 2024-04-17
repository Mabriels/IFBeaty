using IFBeaty.Data;
using IFBeaty.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IFBeaty.Repositorios;

public class ProcedimentoRepositorio
{

  //Contexto injetado no construtor
  private readonly ContextoBD _contexto;

  //Construtor injetando o contexto
  public ProcedimentoRepositorio([FromServices] ContextoBD contexto)
  {
    _contexto = contexto;
  }

  public Procedimento CriarProcedimento(Procedimento procedimento)
  {
    //Mandar salvar o procedimento no contexto
    _contexto.Procedimentos.Add(procedimento);
    _contexto.SaveChanges();

    return procedimento;
  }

  public List<Procedimento> ListarProcedimentos()
  {
    return _contexto.Procedimentos.AsNoTracking().ToList();
  }

  public Procedimento BuscarProcedimentoPeloId(int id, bool tracking = true)
  {

    return tracking ?
      _contexto.Procedimentos.FirstOrDefault(procedimento => procedimento.Id == id) :
      _contexto.Procedimentos.AsNoTracking().FirstOrDefault(procedimento => procedimento.Id == id);

  }

  public void RemoverProcedimento(Procedimento procedimento)
  {
    //Removendo do contexto
    _contexto.Remove(procedimento);

    //Salvando a deleção no BD
    _contexto.SaveChanges();
  }

  public void AtualizarProcedimento()
  {
    //Mandar salvar as mudanças no BD
    _contexto.SaveChanges();
  }
}

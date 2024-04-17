using IFBeaty.Data;
using IFBeaty.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IFBeaty.Repositorios;

public class DiaFuncionamentoRepositorio
{
  //Contexto injetado no construtor
  private readonly ContextoBD _contexto;

  //Construtor injetando o contexto
  public DiaFuncionamentoRepositorio([FromServices] ContextoBD contexto)
  {
    _contexto = contexto;
  }

  public DiaFuncionamento CriarDiaFuncionamento(DiaFuncionamento diaFuncionamento)
  {
    //Mandar salvar o diaFuncionamento no contexto
    _contexto.DiasFuncionamento.Add(diaFuncionamento);
    _contexto.SaveChanges();

    return diaFuncionamento;
  }

  public List<DiaFuncionamento> ListarDiasFuncionamento(bool futuro = false)
  {
    return futuro ?
      _contexto.DiasFuncionamento
        .Where(df => df.Inicio.Date > DateTime.Now.Date)
        .AsNoTracking().ToList() :
      _contexto.DiasFuncionamento.AsNoTracking().ToList();
  }

  public DiaFuncionamento BuscarDiaFuncionamentoPeloId(int id, bool tracking = true)
  {

    return tracking ?
      _contexto.DiasFuncionamento.FirstOrDefault(diaFuncionamento => diaFuncionamento.Id == id) :
      _contexto.DiasFuncionamento.AsNoTracking().FirstOrDefault(diaFuncionamento => diaFuncionamento.Id == id);

  }

  public DiaFuncionamento BuscarDiaFuncionamentoPelaDataInicio(DateTime inicio)
  {
    return _contexto.DiasFuncionamento.AsNoTracking()
      .FirstOrDefault(diaFuncionamento => diaFuncionamento.Inicio.Date == inicio.Date);
  }

  public void RemoverDiaFuncionamento(DiaFuncionamento diaFuncionamento)
  {
    //Removendo do contexto
    _contexto.Remove(diaFuncionamento);

    //Salvando a deleção no BD
    _contexto.SaveChanges();
  }

}

using IFBeaty.Data;
using IFBeaty.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IFBeaty.Repositorios;

public class AgendamentoRepositorio
{
  //Contexto injetado no construtor
  private readonly ContextoBD _contexto;

  //Construtor injetando o contexto
  public AgendamentoRepositorio([FromServices] ContextoBD contexto)
  {
    _contexto = contexto;
  }

  public Agendamento CriaAgendamento(Agendamento agendamento)
  {
    _contexto.Agendamentos.Add(agendamento);
    _contexto.SaveChanges();

    return agendamento;
  }

  public Agendamento BuscarAgendamentoPeloProcedimentoEHorario(int procedimentoId, DateTime horario)
  {
    return _contexto
            .Agendamentos
            .AsNoTracking()
            .FirstOrDefault(agendamento => agendamento.ProcedimentoId == procedimentoId && agendamento.Horario == horario);
  }

  public List<Agendamento> ListarAgendamentos()
  {
    return _contexto
            .Agendamentos
            .Include(a => a.Cliente)
            .Include(a => a.Procedimento)
            .Include(a => a.DiaFuncionamento)
            .AsNoTracking()
            .ToList();
  }

  public Agendamento BuscarAgendamentoPeloId(int id, bool tracking = true)
  {
    return tracking ?
     _contexto.Agendamentos
       .Include(a => a.Cliente)
       .Include(a => a.Procedimento)
       .Include(a => a.DiaFuncionamento)
       .FirstOrDefault(agendamento => agendamento.Id == id) :
     _contexto.Agendamentos
       .AsNoTracking()
       .Include(a => a.Cliente)
       .Include(a => a.Procedimento)
       .Include(a => a.DiaFuncionamento)
       .FirstOrDefault(agendamento => agendamento.Id == id);
  }

  public void RemoverAgendamento(Agendamento agendamento)
  {
    _contexto.Remove(agendamento);
    _contexto.SaveChanges();
  }

  //Retornar todos os agendamentos daquele procedimento naquele dia
  public List<Agendamento> BuscarAgendamentosDoProcedimentoNoDia(int procedimentoId, int diaFuncionamentoId)
  {
    return _contexto.Agendamentos.AsNoTracking()
      .Where(a => a.ProcedimentoId == procedimentoId && a.DiaFuncionamentoId == diaFuncionamentoId)
      .ToList();
  }



}

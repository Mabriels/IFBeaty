using IFBeaty.Dtos.Agendamento;
using IFBeaty.Models;
using IFBeaty.Repositorios;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace IFBeaty.Services;

public class AgendamentoServico
{
  private readonly AgendamentoRepositorio _agendamentoRepositorio;
  private readonly DiaFuncionamentoRepositorio _diaFuncionamentoRepositorio;
  private readonly ProcedimentoRepositorio _procedimentoRepositorio;

  public AgendamentoServico(
      [FromServices] AgendamentoRepositorio repositorio,
      [FromServices] DiaFuncionamentoRepositorio dfRepositorio,
      [FromServices] ProcedimentoRepositorio pRepositorio
    )
  {
    _agendamentoRepositorio = repositorio;
    _diaFuncionamentoRepositorio = dfRepositorio;
    _procedimentoRepositorio = pRepositorio;
  }

  public AgendamentoResposta CriarAgendamento(AgendamentoCriarRequisicao novoAgendamento)
  {
    //Não pode ter um agendamento para o mesmo procedimento e mesmo horário
    var agendamentoExistente =
      _agendamentoRepositorio.BuscarAgendamentoPeloProcedimentoEHorario(novoAgendamento.ProcedimentoId, novoAgendamento.Horario);

    if (agendamentoExistente is not null)
    {
      throw new BadHttpRequestException("Já existe agendamento para esse procedimento nesse horário");
    }

    //Verificar se o horario é um dos horários disponiveis
    var horariosDisponiveis = BuscarHorariosDisponiveis(novoAgendamento.ProcedimentoId, novoAgendamento.DiaFuncionamentoId);

    if (horariosDisponiveis.FirstOrDefault(h => h == novoAgendamento.Horario) == new DateTime())
    {
      throw new BadHttpRequestException("Não pode agendar esse procedimento nesse horário");
    }

    //Copiar dos dados da Requisicao para Modelo
    var agendamento = novoAgendamento.Adapt<Agendamento>();

    //Mandando o repositorio salvar
    agendamento = _agendamentoRepositorio.CriaAgendamento(agendamento);

    //Copiando os dados do Modelo para a Resposta e retornando
    return agendamento.Adapt<AgendamentoResposta>();
  }

  public List<AgendamentoResposta> ListarAgendamentos()
  {
    //Buscando todos os agendamentos
    var agendamentos = _agendamentoRepositorio.ListarAgendamentos();

    //copiar de modelo para resposta e retornar
    return agendamentos.Adapt<List<AgendamentoResposta>>();

  }

  private Agendamento BuscarPeloId(int id, bool tracking = true)
  {
    var agendamento = _agendamentoRepositorio.BuscarAgendamentoPeloId(id, tracking);

    if (agendamento is null)
    {
      throw new Exception("Agendamento não encontrado!");
    }

    return agendamento;
  }

  public AgendamentoResposta BuscarAgendamentoPeloId(int id)
  {
    var agendamento = BuscarPeloId(id, false);

    //Copiar de modelo para resposta e retornar
    return agendamento.Adapt<AgendamentoResposta>();
  }

  public void RemoverAgendamento(int id)
  {
    //Buscando o agendamento que deseja remover pelo id
    var agendamento = BuscarPeloId(id);

    //Nao pode excluir agendamento passado
    if (agendamento.Horario <= DateTime.Now)
    {
      throw new BadHttpRequestException("Não pode excluir agendamento passado!");
    }

    //Mandando o repositorio remover
    _agendamentoRepositorio.RemoverAgendamento(agendamento);

  }

  public List<DateTime> BuscarHorariosDisponiveis(int procedimentoId, int diaFuncionamentoId)
  {
    //Buscar os agendamentos do procedimento para determinado dia
    var agendamentos = _agendamentoRepositorio.BuscarAgendamentosDoProcedimentoNoDia(procedimentoId, diaFuncionamentoId);
    //Buscar as informações do Dia de Funcionamento
    var diaFuncionamento = _diaFuncionamentoRepositorio.BuscarDiaFuncionamentoPeloId(diaFuncionamentoId);
    //Buscar as informações do Procedimento
    var procedimento = _procedimentoRepositorio.BuscarProcedimentoPeloId(procedimentoId);

    //Informações para fazer o calculo
    int horaInicio = diaFuncionamento.Inicio.Hour;
    int horaFim = diaFuncionamento.Fim.Hour;
    TimeSpan diferenca = diaFuncionamento.Fim - diaFuncionamento.Inicio;
    double diferencaEmHoras = diferenca.TotalHours;
    int duracao = procedimento.Duracao;

    //Quantos procedimentos consegue realizar no dia
    var quantidadeProcedimentosNoDia = (int)((diferencaEmHoras * 60) / duracao);

    //Lista de horarios vazia
    List<DateTime> horarios = new();

    for (int i = 0; i < quantidadeProcedimentosNoDia; i++)
    {
      var temp = diaFuncionamento.Inicio.AddMinutes(duracao * i);

      if (agendamentos.FirstOrDefault(x => x.Horario == temp) is null)
      {
        horarios.Add(temp);
      }

    }

    return horarios;

  }


}

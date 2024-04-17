using IFBeaty.Dtos.Agendamento;
using IFBeaty.Services;
using Microsoft.AspNetCore.Mvc;

namespace IFBeaty.Controllers;

[ApiController]
[Route("agendamentos")]
public class AgendamentoController : ControllerBase
{
  private readonly AgendamentoServico _agendamentoServico;
  public AgendamentoController([FromServices] AgendamentoServico servico)
  {
    _agendamentoServico = servico;
  }

  [HttpPost]
  public ActionResult<AgendamentoResposta> PostAgendamento([FromBody] AgendamentoCriarRequisicao novoAgendamento)
  {
    try
    {
      var agendamentoResposta = _agendamentoServico.CriarAgendamento(novoAgendamento);

      return StatusCode(201, agendamentoResposta);
    }
    catch (BadHttpRequestException e)
    {
      return BadRequest(e.Message);
    }

  }

  [HttpGet]
  public ActionResult<List<AgendamentoResposta>> GetAgendamentos()
  {
    return Ok(_agendamentoServico.ListarAgendamentos());
  }


  [HttpGet("{id:int}")]
  public ActionResult<AgendamentoResposta> GetAgendamento([FromRoute] int id)
  {
    try
    {
      return Ok(_agendamentoServico.BuscarAgendamentoPeloId(id));
    }
    catch (Exception e)
    {
      return NotFound(e.Message);
    }
  }


  [HttpDelete("{id:int}")]
  public ActionResult DeleteAgendamento([FromRoute] int id)
  {

    try
    {
      //mandando pro servico excluir
      _agendamentoServico.RemoverAgendamento(id);
      return NoContent();
    }
    catch (BadHttpRequestException e)
    {
      return BadRequest(e.Message);
    }
    catch (Exception e)
    {
      return NotFound(e.Message);
    }
  }

  [HttpGet("horarios/procedimento/{procedimentoId:int}/dia/{diaFuncionamentoId:int}")]
  public ActionResult<List<DateTime>> ListarHorariosDisponiveis([FromRoute] int procedimentoId, [FromRoute] int diaFuncionamentoId)
  {
    return Ok(_agendamentoServico.BuscarHorariosDisponiveis(procedimentoId, diaFuncionamentoId));
  }
}

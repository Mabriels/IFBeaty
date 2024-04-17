using IFBeaty.Dtos.DiaFuncionamento;
using IFBeaty.Services;
using Microsoft.AspNetCore.Mvc;

namespace IFBeaty.Controllers;

[ApiController]
[Route("dias-funcionamento")]
public class DiaFuncionamentoController : ControllerBase
{
  //Campo injetado no construtor
  //vai armazenar o servico que vai ser usado por esse controlador
  private readonly DiaFuncionamentoServico _diaFuncionamentoServico;

  //Construtor com injeção de dependencia
  public DiaFuncionamentoController([FromServices] DiaFuncionamentoServico servico)
  {
    _diaFuncionamentoServico = servico;
  }

  [HttpPost]
  public ActionResult<DiaFuncionamentoResposta> PostDiaFuncionamento
    ([FromBody] DiaFuncionamentoCriarAtualizarRequisicao novoDiaFuncionamento)
  {

    try
    {
      //Enviar para a classe de serviço
      var diaFuncionamentoResposta = _diaFuncionamentoServico.CriarDiaFuncionamento(novoDiaFuncionamento);

      //Retornando resposta para o App Cliente (JSON)
      return CreatedAtAction(nameof(GetDiaFuncionamento), new { id = diaFuncionamentoResposta.Id }, diaFuncionamentoResposta);
    }
    catch (BadHttpRequestException e)
    {

      return BadRequest(e.Message);
    }

  }

  [HttpGet]
  public ActionResult<List<DiaFuncionamentoResposta>> GetDiasFuncionamento([FromQuery] bool futuro = false)
  {
    return Ok(_diaFuncionamentoServico.ListarDiasFuncionamento(futuro));
  }

  // [HttpGet("futuro")]
  // public ActionResult<List<DiaFuncionamentoResposta>> GetDiasFuncionamentoFuturo([From])
  // {
  //   return Ok(_diaFuncionamentoServico.ListarDiasFuncionamento(true));
  // }

  [HttpGet("{id:int}")]
  public ActionResult<DiaFuncionamentoResposta> GetDiaFuncionamento([FromRoute] int id)
  {
    try
    {
      //Pedindo o serviço para buscar pelo id
      return Ok(_diaFuncionamentoServico.BuscarDiaFuncionamentoPeloId(id));
    }
    catch (Exception e)
    {
      return NotFound(e.Message);
    }
  }

  [HttpDelete("{id:int}")]
  public ActionResult DeleteDiaFuncionamento([FromRoute] int id)
  {

    try
    {
      //mandando pro servico excluir
      _diaFuncionamentoServico.RemoverDiaFuncionamento(id);
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


}

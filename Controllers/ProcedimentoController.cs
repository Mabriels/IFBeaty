using IFBeaty.Dtos.Procedimento;
using IFBeaty.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IFBeaty.Controllers;

[ApiController]
[Route("procedimentos")]
// [Authorize]
public class ProcedimentoController : ControllerBase
{
  //Campo injetado no construtor
  //vai armazenar o servico que vai ser usado por esse controlador
  private readonly ProcedimentoServico _procedimentoServico;

  //Construtor com injeção de dependencia
  public ProcedimentoController([FromServices] ProcedimentoServico servico)
  {
    _procedimentoServico = servico;
  }

  [HttpPost]
  public ActionResult<ProcedimentoResposta> PostProcedimento([FromBody] ProcedimentoCriarAtualizarRequisicao novoProcedimento)
  {

    //Enviar para a classe de serviço
    var procedimentoResposta = _procedimentoServico.CriarProcedimento(novoProcedimento);

    //Retornando resposta para o App Cliente (JSON)
    return CreatedAtAction(nameof(GetProcedimento), new { id = procedimentoResposta.Id }, procedimentoResposta);

  }

  [HttpGet]
  // [Authorize(Roles = "Administrador,Atendimento")]
  public ActionResult<List<ProcedimentoResposta>> GetProcedimentos()
  {
    return Ok(_procedimentoServico.ListarProcedimentos());
  }

  [HttpGet("resposta-customizada")]
  public ActionResult TesteResposta()
  {
    return StatusCode(200, new { Resultado = "Deu tudo certo" });
  }

  [AllowAnonymous]
  [HttpGet("{id:int}")]
  public ActionResult<ProcedimentoResposta> GetProcedimento([FromRoute] int id)
  {
    try
    {
      //Pedindo o serviço para buscar o procedimento pelo id
      return Ok(_procedimentoServico.BuscarProcedimentoPeloId(id));
    }
    catch (Exception e)
    {
      return NotFound(e.Message);
    }
  }

  [HttpDelete("{id:int}")]
  public ActionResult DeleteProcedimento([FromRoute] int id)
  {

    try
    {
      //mandando pro servico excluir
      _procedimentoServico.RemoverProcedimento(id);
      return NoContent();
    }
    catch (Exception e)
    {
      return NotFound(e.Message);
    }

  }

  [HttpPut("{id:int}")]
  public ActionResult<ProcedimentoResposta> PutProcedimento([FromRoute] int id,
      [FromBody] ProcedimentoCriarAtualizarRequisicao procedimentoEditado)
  {

    try
    {
      //Enviar para a classe de serviço
      var procedimentoResposta = _procedimentoServico.AtualizarProcedimento(id, procedimentoEditado);

      //Retornando resposta para o App Cliente (JSON)
      return Ok(procedimentoResposta);
    }
    catch (Exception e)
    {
      return NotFound(e.Message);
    }

  }

}
using IFBeaty.Dtos.Usuario;
using IFBeaty.Excecoes;
using IFBeaty.Models;
using IFBeaty.Services;
using Microsoft.AspNetCore.Mvc;

namespace IFBeaty.Controllers;

[ApiController]
[Route("usuarios")]
public class UsuarioController : ControllerBase
{
  //injetado no construtor
  private readonly UsuarioServico _usuarioServico;

  //Construtor com injeção de dependencia
  public UsuarioController([FromServices] UsuarioServico servico)
  {
    _usuarioServico = servico;
  }

  [HttpPost]
  public ActionResult<UsuarioResposta> PostUsuario([FromBody] UsuarioCriarRequisicao novoUsuario)
  {

    try
    {
      //Enviar para a classe de serviço
      var usuarioResposta = _usuarioServico.CriarUsuario(novoUsuario);

      //Retornando resposta para o App Cliente (JSON)
      return CreatedAtAction(nameof(GetUsuario), new { id = usuarioResposta.Id }, usuarioResposta);
    }
    catch (EmailExistenteException e)
    {
      return BadRequest(e.Message);
    }

  }

  [HttpGet]
  public ActionResult<List<UsuarioResposta>> GetUsuarios()
  {
    return Ok(_usuarioServico.ListarUsuarios());
  }

  [HttpGet("{id:int}")]
  public ActionResult<UsuarioResposta> GetUsuario([FromRoute] int id)
  {
    try
    {
      //Pedindo o serviço para buscar o procedimento pelo id
      return Ok(_usuarioServico.BuscarUsuarioPeloId(id));
    }
    catch (Exception e)
    {
      return NotFound(e.Message);
    }
  }

  [HttpDelete("{id:int}")]
  public ActionResult DeleteUsuario([FromRoute] int id)
  {

    try
    {
      //mandando pro servico excluir
      _usuarioServico.RemoverUsuario(id);
      return NoContent();
    }
    catch (Exception e)
    {
      return NotFound(e.Message);
    }

  }

  [HttpPut("{id:int}")]
  public ActionResult<UsuarioResposta> PutProcedimento([FromRoute] int id,
      [FromBody] UsuarioAtualizarRequisicao usuarioEditado)
  {

    try
    {
      //Enviar para a classe de serviço
      var usuarioResposta = _usuarioServico.AtualizarUsuario(id, usuarioEditado);

      //Retornando resposta para o App Cliente (JSON)
      return Ok(usuarioResposta);
    }
    catch (EmailExistenteException e)
    {
      return BadRequest(e.Message);
    }
    catch (Exception e)
    {
      return NotFound(e.Message);
    }

  }

  [HttpPost("{usuarioId:int}/perfil/{perfilId:int}")]
  public ActionResult<UsuarioResposta> PostUsuarioPerfis([FromRoute] int usuarioId, [FromRoute] int perfilId)
  {
    try
    {
      return Ok(_usuarioServico.AtribuirPerfil(usuarioId, perfilId));
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


  [HttpDelete("{usuarioId:int}/perfil/{perfilId:int}")]
  public ActionResult<UsuarioResposta> DeleteUsuarioPerfis([FromRoute] int usuarioId, [FromRoute] int perfilId)
  {
    try
    {
      System.Console.WriteLine("Removendo");
      return Ok(_usuarioServico.RemoverPerfil(usuarioId, perfilId));
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


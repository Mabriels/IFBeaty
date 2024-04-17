using IFBeaty.Dtos.Procedimento;
using IFBeaty.Models;
using IFBeaty.Repositorios;
using Microsoft.AspNetCore.Mvc;

using Mapster;
using System.Security.Claims;

namespace IFBeaty.Services;

public class ProcedimentoServico
{
  //Repositorio injetado no construtor
  private readonly ProcedimentoRepositorio _procedimentoRepositorio;

  //Construtor com injecao do repositorio
  public ProcedimentoServico([FromServices] ProcedimentoRepositorio repositorio)
  {
    _procedimentoRepositorio = repositorio;
  }


  public ProcedimentoResposta CriarProcedimento
    (ProcedimentoCriarAtualizarRequisicao novoProcedimento)
  {

    //Copiar dos dados da Requisicao para Modelo
    var procedimento = novoProcedimento.Adapt<Procedimento>();

    //Regras específicas
    var agora = DateTime.Now;
    procedimento.DataCriacao = agora;
    procedimento.DataAtualizacao = agora;

    //Mandando o repositorio salavar o modelo no BD
    procedimento = _procedimentoRepositorio.CriarProcedimento(procedimento);

    //Copiando os dados do Modelo para a Resposta
    var procedimentoResposta = procedimento.Adapt<ProcedimentoResposta>();

    //Retornando a resposta pro controlador
    return procedimentoResposta;
  }

  public List<ProcedimentoResposta> ListarProcedimentos()
  {
    //pegando todos procedimentos (modelos)
    var procedimentos = _procedimentoRepositorio.ListarProcedimentos();

    //copiar de modelo para resposta
    var procedimentoRespostas = procedimentos.Adapt<List<ProcedimentoResposta>>();

    return procedimentoRespostas;
  }

  public ProcedimentoResposta BuscarProcedimentoPeloId(int id)
  {
    //Buscar o modelo pelo ID
    var procedimento = BuscarPeloId(id, false);

    //Copiando modelo para resposta
    var procedimentoResposta = procedimento.Adapt<ProcedimentoResposta>();

    //Retornndo pro controlador
    return procedimentoResposta;
  }

  public void RemoverProcedimento(int id)
  {
    //Se tiver alguma regra de negócio de exclusao colocar antes

    //Buscando o procedimento que deseja remover pelo id
    var procedimento = BuscarPeloId(id);

    //Mandando o repositorio remover
    _procedimentoRepositorio.RemoverProcedimento(procedimento);

  }

  public ProcedimentoResposta AtualizarProcedimento(int id, ProcedimentoCriarAtualizarRequisicao procedimentoEditado)
  {

    //Buscar o procedimento (modelo) no repositório pelo id para poder editá-lo
    var procedimento = BuscarPeloId(id);

    //Copiar dos dados da Requisicao para Modelo
    procedimentoEditado.Adapt(procedimento);

    //Regras específicas
    procedimento.DataAtualizacao = DateTime.Now;

    //Mandando o repositorio salvar as alteracoes no BD
    _procedimentoRepositorio.AtualizarProcedimento();

    //Copiando os dados do Modelo para a Resposta
    var procedimentoResposta = procedimento.Adapt<ProcedimentoResposta>();

    //Retornando a resposta pro controlador
    return procedimentoResposta;
  }

  private Procedimento BuscarPeloId(int id, bool tracking = true)
  {
    var procedimento = _procedimentoRepositorio.BuscarProcedimentoPeloId(id, tracking);

    if (procedimento is null)
    {
      throw new Exception("Procedimento não encontrado!");
    }

    return procedimento;
  }
}

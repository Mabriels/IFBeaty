using IFBeaty.Dtos.DiaFuncionamento;
using IFBeaty.Models;
using IFBeaty.Repositorios;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace IFBeaty.Services;

public class DiaFuncionamentoServico
{
  //Repositorio injetado no construtor
  private readonly DiaFuncionamentoRepositorio _diaFuncionamentoRepositorio;

  //Construtor com injecao do repositorio
  public DiaFuncionamentoServico([FromServices] DiaFuncionamentoRepositorio repositorio)
  {
    _diaFuncionamentoRepositorio = repositorio;
  }

  public DiaFuncionamentoResposta CriarDiaFuncionamento(DiaFuncionamentoCriarAtualizarRequisicao novoDiaFuncionamento)
  {

    //Não pode cadastrar data passada
    if (novoDiaFuncionamento.Inicio.Date < DateTime.Now.Date)
    {
      throw new BadHttpRequestException("Não pode cadastrar dia de funcionamento no passado");
    }

    //Inicio e Fim tem que ser na mesma data
    if (novoDiaFuncionamento.Inicio.Date != novoDiaFuncionamento.Fim.Date)
    {
      throw new BadHttpRequestException("Inicio e Fim tem que ser no mesmo dia");
    }

    //Deve ter no mínimo 1 hora de funcionamento no dia
    var tempofuncionamento = novoDiaFuncionamento.Fim - novoDiaFuncionamento.Inicio;
    if (tempofuncionamento.TotalHours < 1)
    {
      throw new BadHttpRequestException("Deve ter no mínimo 1 hora de funcionamento no dia");
    }

    //Não pode ter outro dia cadastrado igual
    var diaFuncionamentoExistente = _diaFuncionamentoRepositorio.BuscarDiaFuncionamentoPelaDataInicio(novoDiaFuncionamento.Inicio);
    if (diaFuncionamentoExistente is not null)
    {
      throw new BadHttpRequestException("Esse dia de funcionamento já está cadastrado");
    }


    //Copiar dos dados da Requisicao para Modelo
    var diaFuncionamento = novoDiaFuncionamento.Adapt<DiaFuncionamento>();

    //Regras específicas

    //Mandando o repositorio salavar o modelo no BD
    diaFuncionamento = _diaFuncionamentoRepositorio.CriarDiaFuncionamento(diaFuncionamento);

    //Copiando os dados do Modelo para a Resposta
    var diaFuncionamentoResposta = diaFuncionamento.Adapt<DiaFuncionamentoResposta>();

    //Retornando a resposta pro controlador
    return diaFuncionamentoResposta;
  }

  public List<DiaFuncionamentoResposta> ListarDiasFuncionamento(bool futuro = false)
  {
    //pegando todos diasFuncionamento (modelos)
    var diasFuncionamento = _diaFuncionamentoRepositorio.ListarDiasFuncionamento(futuro);

    //copiar de modelo para resposta
    var diasFuncionamentoRespostas = diasFuncionamento.Adapt<List<DiaFuncionamentoResposta>>();

    return diasFuncionamentoRespostas;
  }

  public DiaFuncionamentoResposta BuscarDiaFuncionamentoPeloId(int id)
  {
    //Buscar o modelo pelo ID
    var diaFuncionamento = BuscarPeloId(id, false);

    //Copiando modelo para resposta
    var diaFuncionamentoResposta = diaFuncionamento.Adapt<DiaFuncionamentoResposta>();

    //Retornndo pro controlador
    return diaFuncionamentoResposta;
  }

  public void RemoverDiaFuncionamento(int id)
  {
    //Buscando o diaFuncionamento que deseja remover pelo id
    var diaFuncionamento = BuscarPeloId(id);

    //Nao pode excluir dia de funcionamento passado ou atual
    if (diaFuncionamento.Inicio.Date <= DateTime.Now.Date)
    {
      throw new BadHttpRequestException("Não pode excluir Dia de Funcionamento menor ou igual a data atual");
    }

    //Mandando o repositorio remover
    _diaFuncionamentoRepositorio.RemoverDiaFuncionamento(diaFuncionamento);

  }

  private DiaFuncionamento BuscarPeloId(int id, bool tracking = true)
  {
    var diaFuncionamento = _diaFuncionamentoRepositorio.BuscarDiaFuncionamentoPeloId(id, tracking);

    if (diaFuncionamento is null)
    {
      throw new Exception("Dia de Funcionamento não encontrado!");
    }

    return diaFuncionamento;
  }
}

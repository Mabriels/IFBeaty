using System.ComponentModel.DataAnnotations;

namespace IFBeaty.Dtos.DiaFuncionamento;

public class DiaFuncionamentoCriarAtualizarRequisicao
{
  public DateTime Inicio { get; set; }
  public DateTime Fim { get; set; }
}

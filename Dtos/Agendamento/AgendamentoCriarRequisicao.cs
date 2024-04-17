using System.ComponentModel.DataAnnotations;

namespace IFBeaty.Dtos.Agendamento;

public class AgendamentoCriarRequisicao
{

  [Required]
  public DateTime Horario { get; set; }

  public bool? Confirmado { get; set; }

  [Required]
  public int ProcedimentoId { get; set; }

  [Required]
  public int DiaFuncionamentoId { get; set; }

  [Required]
  public int ClienteId { get; set; }
}

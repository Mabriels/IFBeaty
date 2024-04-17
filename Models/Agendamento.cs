using System.ComponentModel.DataAnnotations;

namespace IFBeaty.Models;

public class Agendamento
{
  [Required]
  public int Id { get; set; }

  [Required]
  public DateTime Horario { get; set; }

  public bool? Confirmado { get; set; }

  [Required]
  public DateTime DataCriacao { get; set; }

  //Propriedade Navegacao
  public Procedimento Procedimento { get; set; }

  //Chave Estrangeira
  public int ProcedimentoId { get; set; }

  //Propriedade Navegacao
  public DiaFuncionamento DiaFuncionamento { get; set; }

  //Chave Estrangeira
  public int DiaFuncionamentoId { get; set; }

  //Propriedade Navegacao
  public Usuario Cliente { get; set; }

  //Chave Estrangeira
  public int ClienteId { get; set; }

}

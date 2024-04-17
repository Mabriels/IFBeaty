using IFBeaty.Dtos.DiaFuncionamento;
using IFBeaty.Dtos.Procedimento;
using IFBeaty.Dtos.Usuario;

namespace IFBeaty.Dtos.Agendamento;

public class AgendamentoResposta
{
  public int Id { get; set; }
  public DateTime Horario { get; set; }
  public bool? Confirmado { get; set; }
  public DateTime DataCriacao { get; set; }
  public ProcedimentoResposta Procedimento { get; set; }
  public int ProcedimentoId { get; set; }
  public DiaFuncionamentoResposta DiaFuncionamento { get; set; }
  public int DiaFuncionamentoId { get; set; }
  public UsuarioResposta Cliente { get; set; }
  public int ClienteId { get; set; }
}

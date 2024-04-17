using System.ComponentModel.DataAnnotations;

namespace IFBeaty.Dtos.Procedimento;

public class ProcedimentoCriarAtualizarRequisicao
{
  [Required(ErrorMessage = "{0} é obrigatório")]
  [StringLength(100, MinimumLength = 3, ErrorMessage = "{0} tem que ter entre {2} e {1} caracteres")]
  public string Nome { get; set; }

  [Required(ErrorMessage = "Duração é obrigatório")]
  [Range(1, 1000, ErrorMessage = "O valor de Duração deve ser entre {1} e {2}")]
  public int? Duracao { get; set; }

  [Required(ErrorMessage = "Preço é obrigatório")]
  [Range(0, 1_000_000)]
  public decimal? Preco { get; set; }

  [Required(ErrorMessage = "Descrição é obrigatório")]
  public string Descricao { get; set; }

  // [Required]
  // // // // [Display(Name = "Data do pedido")]
  // // // // [DisplayFormat(DataFormatString = "mm/dd/yyyy")]
  // // // [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
  // [RegularExpression(@"^\d{4}-\d{2}-\d{2}?(.*)$")]
  // // // [Required]
  // // [DataType(DataType.DateTime)]
  // // // [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")] // or something similar
  // // // [DataType]
  // public DateTime? Data { get; set; }


}

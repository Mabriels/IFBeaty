using System.ComponentModel.DataAnnotations;


using System.ComponentModel.DataAnnotations.Schema;

namespace IFBeaty.Models;

public class Procedimento
{
  [Required]
  public int Id { get; set; }

  [Required]
  [Column(TypeName = "varchar(100)")]
  public string Nome { get; set; }

  [Required]
  public int Duracao { get; set; }

  [Required]
  [Column(TypeName = "decimal(13,2)")]
  public decimal Preco { get; set; }

  [Required]
  [Column(TypeName = "text")]
  public string Descricao { get; set; }

  [Required]
  public DateTime DataCriacao { get; set; }

  [Required]
  public DateTime DataAtualizacao { get; set; }

  //Propriedade de Navegação (Coleção)
  public List<Agendamento> Agendamentos { get; set; }


}


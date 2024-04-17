using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IFBeaty.Models;

public class Endereco
{
  [Required]
  public int Id { get; set; }

  [Required]
  [Column(TypeName = "varchar(100)")]
  public string Rua { get; set; }

  [Required]
  [Column(TypeName = "varchar(20)")]
  public string Numero { get; set; }

  [Column(TypeName = "varchar(10)")]
  public string? Complemento { get; set; }

  [Required]
  [Column(TypeName = "varchar(30)")]
  public string Bairro { get; set; }

  [Required]
  [Column(TypeName = "varchar(50)")]
  public string Cidade { get; set; }

  [Required]
  [Column(TypeName = "varchar(9)")]
  public string CEP { get; set; }

  //Propriedade de Navegação
  public Usuario Usuario { get; set; }

  //Chave Estrangeira
  public int UsuarioId { get; set; }
}

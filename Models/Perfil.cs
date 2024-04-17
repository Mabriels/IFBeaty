using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IFBeaty.Models;

public class Perfil
{
  [Required]
  public int Id { get; set; }

  [Required]
  [Column(TypeName = "varchar(30)")]
  public string Nome { get; set; }

  //Propriedade Navegacao
  public List<Usuario> Usuarios { get; set; }
}


using System.ComponentModel.DataAnnotations;

namespace IFBeaty.Dtos.Usuario;

public class UsuarioLoginRequisicao
{

  [Required]
  [StringLength(50, MinimumLength = 3)]
  public string Email { get; set; }

  [Required]
  [StringLength(60, MinimumLength = 3)]
  public string Senha { get; set; }
}

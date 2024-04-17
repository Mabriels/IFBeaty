using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IFBeaty.Models;

[Index(nameof(Email), IsUnique = true)]
public class Usuario
{
  [Required]
  public int Id { get; set; }

  [Required]
  [Column(TypeName = "varchar(100)")]
  public string Nome { get; set; }

  [Required]
  [Column(TypeName = "varchar(50)")]
  public string Email { get; set; }

  [Required]
  [Column(TypeName = "varchar(60)")]
  public string Senha { get; set; }

  [Required]
  [Column(TypeName = "varchar(20)")]
  public string Telefone { get; set; }

  //Propriedade de Navegacao
  public Endereco Endereco { get; set; }

  //Propriedade Navegacao
  public List<Agendamento> Agendamentos { get; set; }

  //Propriedade Navegacao
  public List<Perfil> Perfis { get; set; }
}

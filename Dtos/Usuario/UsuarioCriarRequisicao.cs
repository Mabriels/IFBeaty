using System.ComponentModel.DataAnnotations;

namespace IFBeaty.Dtos.Usuario;

public class UsuarioCriarRequisicao
{
  [StringLength(100, MinimumLength = 3)]
  public string Nome { get; set; }
  public string Email { get; set; }
  public string Senha { get; set; }
  public string Telefone { get; set; }
  public EnderecoCriarAtualizarRequisicao Endereco { get; set; }
}

public class EnderecoCriarAtualizarRequisicao
{
  public string Rua { get; set; }
  public string Numero { get; set; }
  public string Complemento { get; set; }
  public string Bairro { get; set; }
  public string Cidade { get; set; }
  public string CEP { get; set; }
}

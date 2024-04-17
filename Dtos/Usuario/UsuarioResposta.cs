namespace IFBeaty.Dtos.Usuario;

public class UsuarioResposta
{
  public int Id { get; set; }
  public string Nome { get; set; }
  public string Email { get; set; }
  public string Telefone { get; set; }
  public EnderecoResposta Endereco { get; set; }
  public List<PerfilResposta> Perfis { get; set; }
}

public class EnderecoResposta
{
  public int Id { get; set; }
  public string Rua { get; set; }
  public string Numero { get; set; }
  public string Complemento { get; set; }
  public string Bairro { get; set; }
  public string Cidade { get; set; }
  public string CEP { get; set; }
}

public class PerfilResposta
{
  public int Id { get; set; }
  public string Nome { get; set; }
}

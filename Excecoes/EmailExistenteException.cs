namespace IFBeaty.Excecoes;

public class EmailExistenteException : Exception
{
  public EmailExistenteException() : base("Já existe usuário com mesmo email")
  {

  }
}



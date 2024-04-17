namespace IFBeaty.Excecoes;

public class BadRequestException : Exception
{
  public BadRequestException(string mensagem) : base(mensagem)
  {

  }
}

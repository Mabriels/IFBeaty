using IFBeaty.Models;
using Microsoft.EntityFrameworkCore;

namespace IFBeaty.Data;

public class ContextoBD : DbContext
{

  //Construtor que vai receber configurações de acesso ao BD
  //Essas configurações virão do Program.cs
  public ContextoBD(DbContextOptions<ContextoBD> options) : base(options)
  {

  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    //Está usando a Fluent API do EF CORE
    modelBuilder.Entity<Perfil>().HasData(
      new Perfil
      {
        Id = 1,
        Nome = "Administrador"
      },
      new Perfil
      {
        Id = 2,
        Nome = "Atendente"
      },
      new Perfil
      {
        Id = 3,
        Nome = "Cliente"
      }
    );

    modelBuilder.Entity<Usuario>().HasData(
     new Usuario
     {
       Id = 1,
       Nome = "Luciano",
       Email = "admin@example.com",
       Senha = BCrypt.Net.BCrypt.HashPassword("123456"),
       Telefone = "38 99999-9999"
     }
    );

    modelBuilder.Entity<Endereco>().HasData(
     new Endereco
     {
       Id = 1,
       Rua = "Humberto Mallard",
       Numero = "350",
       Bairro = "Santos Dumont",
       Cidade = "Pirapora",
       CEP = "39270-000",
       UsuarioId = 1
     }
    );

    modelBuilder
    .Entity<Usuario>()
    .HasMany(u => u.Perfis)
    .WithMany(p => p.Usuarios)
    .UsingEntity(pu => pu.HasData(new { PerfisId = 1, UsuariosId = 1 }));


    //  modelBuilder.Entity<Author>().HasData(
    //     new Author
    //     {
    //         AuthorId = 1,
    //         FirstName = "William",
    //         LastName = "Shakespeare"
    //     }
    // );

    // modelBuilder.Entity<Book>().HasData(
    //     new Book { BookId = 1, AuthorId = 1, Title = "Hamlet" },
    //     new Book { BookId = 2, AuthorId = 1, Title = "King Lear" },
    //     new Book { BookId = 3, AuthorId = 1, Title = "Othello" }
    // );
  }

  //TABELAS
  public DbSet<Procedimento> Procedimentos { get; set; }
  public DbSet<Agendamento> Agendamentos { get; set; }
  public DbSet<DiaFuncionamento> DiasFuncionamento { get; set; }
  public DbSet<Endereco> Enderecos { get; set; }
  public DbSet<Perfil> Perfis { get; set; }
  public DbSet<Usuario> Usuarios { get; set; }
  public Agendamento AsnoTracking { get; internal set; }
}

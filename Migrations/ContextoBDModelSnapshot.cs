﻿// <auto-generated />
using System;
using IFBeaty.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IFBeaty.Migrations
{
    [DbContext(typeof(ContextoBD))]
    partial class ContextoBDModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("IFBeaty.Models.Agendamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<bool?>("Confirmado")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("DiaFuncionamentoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Horario")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ProcedimentoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("DiaFuncionamentoId");

                    b.HasIndex("ProcedimentoId");

                    b.ToTable("Agendamentos");
                });

            modelBuilder.Entity("IFBeaty.Models.DiaFuncionamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Fim")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Inicio")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("DiasFuncionamento");
                });

            modelBuilder.Entity("IFBeaty.Models.Endereco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<string>("CEP")
                        .IsRequired()
                        .HasColumnType("varchar(9)");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Complemento")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Rua")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId")
                        .IsUnique();

                    b.ToTable("Enderecos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Bairro = "Santos Dumont",
                            CEP = "39270-000",
                            Cidade = "Pirapora",
                            Numero = "350",
                            Rua = "Humberto Mallard",
                            UsuarioId = 1
                        });
                });

            modelBuilder.Entity("IFBeaty.Models.Perfil", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Perfis");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nome = "Administrador"
                        },
                        new
                        {
                            Id = 2,
                            Nome = "Atendente"
                        },
                        new
                        {
                            Id = 3,
                            Nome = "Cliente"
                        });
                });

            modelBuilder.Entity("IFBeaty.Models.Procedimento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DataAtualizacao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Duracao")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal(13,2)");

                    b.HasKey("Id");

                    b.ToTable("Procedimentos");
                });

            modelBuilder.Entity("IFBeaty.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("varchar(60)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Usuarios");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin@example.com",
                            Nome = "Luciano",
                            Senha = "$2a$11$hBsx2VUigERY5LxPDtVTOeYB8qZqmqk3jk.qQgOpo/MP3zg/n/Nee",
                            Telefone = "38 99999-9999"
                        });
                });

            modelBuilder.Entity("PerfilUsuario", b =>
                {
                    b.Property<int>("PerfisId")
                        .HasColumnType("int");

                    b.Property<int>("UsuariosId")
                        .HasColumnType("int");

                    b.HasKey("PerfisId", "UsuariosId");

                    b.HasIndex("UsuariosId");

                    b.ToTable("PerfilUsuario");

                    b.HasData(
                        new
                        {
                            PerfisId = 1,
                            UsuariosId = 1
                        });
                });

            modelBuilder.Entity("IFBeaty.Models.Agendamento", b =>
                {
                    b.HasOne("IFBeaty.Models.Usuario", "Cliente")
                        .WithMany("Agendamentos")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IFBeaty.Models.DiaFuncionamento", "DiaFuncionamento")
                        .WithMany("Agendamentos")
                        .HasForeignKey("DiaFuncionamentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IFBeaty.Models.Procedimento", "Procedimento")
                        .WithMany("Agendamentos")
                        .HasForeignKey("ProcedimentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("DiaFuncionamento");

                    b.Navigation("Procedimento");
                });

            modelBuilder.Entity("IFBeaty.Models.Endereco", b =>
                {
                    b.HasOne("IFBeaty.Models.Usuario", "Usuario")
                        .WithOne("Endereco")
                        .HasForeignKey("IFBeaty.Models.Endereco", "UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("PerfilUsuario", b =>
                {
                    b.HasOne("IFBeaty.Models.Perfil", null)
                        .WithMany()
                        .HasForeignKey("PerfisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IFBeaty.Models.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UsuariosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IFBeaty.Models.DiaFuncionamento", b =>
                {
                    b.Navigation("Agendamentos");
                });

            modelBuilder.Entity("IFBeaty.Models.Procedimento", b =>
                {
                    b.Navigation("Agendamentos");
                });

            modelBuilder.Entity("IFBeaty.Models.Usuario", b =>
                {
                    b.Navigation("Agendamentos");

                    b.Navigation("Endereco");
                });
#pragma warning restore 612, 618
        }
    }
}
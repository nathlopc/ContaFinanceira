﻿// <auto-generated />
using System;
using ContaFinanceira.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ContaFinanceira.Persistance.Migrations
{
    [DbContext(typeof(SqlServerContext))]
    partial class SqlServerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.14")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ContaFinanceira.Domain.Entities.Agencia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("tbcf_agencias");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nome = "Agência 1"
                        },
                        new
                        {
                            Id = 2,
                            Nome = "Agência 2"
                        });
                });

            modelBuilder.Entity("ContaFinanceira.Domain.Entities.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ContaId")
                        .HasColumnType("int");

                    b.Property<string>("CpfCnpj")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TipoPessoa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ContaId")
                        .IsUnique();

                    b.HasIndex("Id", "ContaId");

                    b.ToTable("tbcf_clientes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ContaId = 1,
                            CpfCnpj = "51865798916",
                            Nome = "Nathália Lopes",
                            TipoPessoa = "PessoaFisica"
                        });
                });

            modelBuilder.Entity("ContaFinanceira.Domain.Entities.Conta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AgenciaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AgenciaId");

                    b.HasIndex("Id");

                    b.ToTable("tbcf_contas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AgenciaId = 1,
                            DataCriacao = new DateTime(2022, 2, 11, 23, 18, 0, 0, DateTimeKind.Unspecified),
                            Senha = "$2a$11$4/WPXcRIcujksmWmnV6bBehoyugcezsR/wQ3Gq1zOKSi0WYuI8svm"
                        });
                });

            modelBuilder.Entity("ContaFinanceira.Domain.Entities.Transacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ContaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ContaId");

                    b.HasIndex("Id", "Data", "Valor");

                    b.ToTable("tbcf_transacoes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ContaId = 1,
                            Data = new DateTime(2022, 2, 11, 23, 18, 0, 0, DateTimeKind.Unspecified),
                            Valor = 10m
                        });
                });

            modelBuilder.Entity("ContaFinanceira.Domain.Entities.Cliente", b =>
                {
                    b.HasOne("ContaFinanceira.Domain.Entities.Conta", "Conta")
                        .WithOne("Cliente")
                        .HasForeignKey("ContaFinanceira.Domain.Entities.Cliente", "ContaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Conta");
                });

            modelBuilder.Entity("ContaFinanceira.Domain.Entities.Conta", b =>
                {
                    b.HasOne("ContaFinanceira.Domain.Entities.Agencia", "Agencia")
                        .WithMany("Contas")
                        .HasForeignKey("AgenciaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Agencia");
                });

            modelBuilder.Entity("ContaFinanceira.Domain.Entities.Transacao", b =>
                {
                    b.HasOne("ContaFinanceira.Domain.Entities.Conta", "Conta")
                        .WithMany("Transacoes")
                        .HasForeignKey("ContaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Conta");
                });

            modelBuilder.Entity("ContaFinanceira.Domain.Entities.Agencia", b =>
                {
                    b.Navigation("Contas");
                });

            modelBuilder.Entity("ContaFinanceira.Domain.Entities.Conta", b =>
                {
                    b.Navigation("Cliente");

                    b.Navigation("Transacoes");
                });
#pragma warning restore 612, 618
        }
    }
}

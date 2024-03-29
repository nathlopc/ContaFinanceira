﻿using ContaFinanceira.Domain.Entities;
using ContaFinanceira.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContaFinanceira.Persistance.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("tbcf_clientes")
                   .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.Nome)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(x => x.TipoPessoa)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(x => x.CpfCnpj)
                   .HasMaxLength(14)
                   .IsRequired();

            builder.Property(x => x.Email)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.HasIndex(x => new { x.Id, x.ContaId });

            builder.HasIndex(x => x.Email)
                   .IsUnique();

            builder.HasData(new Cliente() 
            { 
                Id = 1,
                Nome = "Nathália Lopes",
                Email = "nathalialcoimbra@gmail.com",
                ContaId = 1,
                TipoPessoa = ePessoa.PessoaFisica,
                CpfCnpj = "51865798916"
            });
        }
    }
}

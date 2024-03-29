﻿using ContaFinanceira.Domain.Entities;
using ContaFinanceira.Domain.Interfaces.Repositories;
using ContaFinanceira.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContaFinanceira.Persistance.Repositories
{
    public class AgenciaRepository : IAgenciaRepository
    {
        internal SqlServerContext _context;

        public AgenciaRepository(SqlServerContext context)
        {
            _context = context;
        }

        public async Task<List<Agencia>> Listar()
        {
            return await _context.Agencias
                                 .AsNoTracking()
                                 .ToListAsync();
        }

        public async Task<Agencia> Pesquisar(int id)
        {
            return await _context.Agencias
                                 .AsNoTracking()
                                 .Where(x => x.Id == id)
                                 .FirstOrDefaultAsync();
        }
    }
}

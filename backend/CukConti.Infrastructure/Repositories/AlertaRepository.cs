using System;
using System.Threading.Tasks;
using CukConti.Domain.Entities;
using CukConti.Domain.Interfaces;
using CukConti.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CukConti.Infrastructure.Repositories
{
    public class AlertaRepository : RepositorioBase<Alerta>, IAlertaRepository
    {
        public AlertaRepository(CukContiDbContext context) : base(context)
        {
        }

        public async Task<bool> ExisteHoyAsync(int contratoId, int diasAntelacion)
        {
            var hoy = DateTime.UtcNow.Date;
            return await _dbSet.AnyAsync(a =>
                a.ContratoId == contratoId &&
                a.DiasAntelacion == diasAntelacion &&
                a.FechaGeneracion.Date == hoy);
        }
    }
}

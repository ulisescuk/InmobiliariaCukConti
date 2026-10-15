using System;
using System.Linq;
using System.Threading.Tasks;
using CukConti.Domain.Entities;
using CukConti.Domain.Enums;
using CukConti.Domain.Interfaces;
using CukConti.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CukConti.Infrastructure.Repositories
{
    public class MovimientoGeneralRepository : RepositorioBase<MovimientoGeneral>, IMovimientoGeneralRepository
    {
        public MovimientoGeneralRepository(CukContiDbContext context) : base(context)
        {
        }

        public async Task<decimal> SumarDelMesPorTipoAsync(TipoMovimiento tipo)
        {
            var hoy = DateTime.UtcNow.Date;
            var inicioDeMes = new DateTime(hoy.Year, hoy.Month, 1);

            return await _dbSet
                .Where(m => m.Tipo == tipo && m.Fecha >= inicioDeMes && m.Fecha <= hoy)
                .SumAsync(m => (decimal?)m.Monto) ?? 0;
        }
    }
}

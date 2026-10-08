using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CukConti.Domain.Entities;

namespace CukConti.Domain.Interfaces
{
    public interface IValorIndiceRepository : IRepositorioBase<ValorIndice>
    {
        Task<DateTime?> ObtenerUltimoPeriodoAsync(int indiceId);
        Task<IEnumerable<ValorIndice>> ListarPorIndiceAsync(int indiceId);
    }
}

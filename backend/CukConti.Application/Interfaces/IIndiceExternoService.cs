using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CukConti.Application.Interfaces
{
    public record ValorIndiceExterno(DateTime Fecha, decimal Valor);

    public interface IIndiceExternoService
    {
        Task<List<ValorIndiceExterno>> ObtenerValoresAsync(int idVariableBcra, DateTime desde, DateTime hasta);
    }
}

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CukConti.Application.Interfaces;

namespace CukConti.Infrastructure.Security
{
    public class BcraIndiceExternoService : IIndiceExternoService
    {
        private readonly HttpClient _httpClient;

        public BcraIndiceExternoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private class BcraResponse
        {
            [JsonPropertyName("results")]
            public List<BcraVariableResultado> Results { get; set; } = new();
        }

        private class BcraVariableResultado
        {
            [JsonPropertyName("idVariable")]
            public int IdVariable { get; set; }

            [JsonPropertyName("detalle")]
            public List<BcraDetalle> Detalle { get; set; } = new();
        }

        private class BcraDetalle
        {
            [JsonPropertyName("fecha")]
            public DateTime Fecha { get; set; }

            [JsonPropertyName("valor")]
            public decimal Valor { get; set; }
        }

        public async Task<List<ValorIndiceExterno>> ObtenerValoresAsync(int idVariableBcra, DateTime desde, DateTime hasta)
        {
            var url = $"estadisticas/v4.0/monetarias/{idVariableBcra}?desde={desde:yyyy-MM-dd}&hasta={hasta:yyyy-MM-dd}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<BcraResponse>(json);

            var resultado = new List<ValorIndiceExterno>();
            if (data?.Results is not null)
            {
                foreach (var variable in data.Results)
                {
                    foreach (var d in variable.Detalle)
                    {
                        resultado.Add(new ValorIndiceExterno(d.Fecha, d.Valor));
                    }
                }
            }

            return resultado;
        }
    }
}

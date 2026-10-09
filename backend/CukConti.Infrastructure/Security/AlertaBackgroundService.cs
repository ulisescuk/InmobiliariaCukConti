using System;
using System.Threading;
using System.Threading.Tasks;
using CukConti.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CukConti.Infrastructure.Security
{
    public class AlertaBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<AlertaBackgroundService> _logger;
        private static readonly TimeSpan HoraDeEjecucion = new TimeSpan(8, 0, 0);

        public AlertaBackgroundService(IServiceScopeFactory scopeFactory, ILogger<AlertaBackgroundService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var ahora = DateTime.UtcNow;
                var proximaEjecucion = ahora.Date + HoraDeEjecucion;
                if (proximaEjecucion <= ahora)
                    proximaEjecucion = proximaEjecucion.AddDays(1);

                var espera = proximaEjecucion - ahora;
                _logger.LogInformation("Proxima generacion de alertas programada para {Fecha}", proximaEjecucion);

                try
                {
                    await Task.Delay(espera, stoppingToken);
                }
                catch (TaskCanceledException)
                {
                    break;
                }

                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var alertaService = scope.ServiceProvider.GetRequiredService<AlertaService>();
                    var resultado = await alertaService.GenerarAlertasDiariasAsync();
                    _logger.LogInformation(
                        "Generacion diaria de alertas completada: {Evaluados} contratos evaluados, {Alertas} alertas creadas, {Mails} mails enviados",
                        resultado.ContratosEvaluados, resultado.AlertasCreadas, resultado.MailsEnviados);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al ejecutar la generacion diaria de alertas");
                }
            }
        }
    }
}

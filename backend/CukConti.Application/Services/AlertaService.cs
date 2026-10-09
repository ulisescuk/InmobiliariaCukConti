using System;
using System.Linq;
using System.Threading.Tasks;
using CukConti.Application.Interfaces;
using CukConti.Domain.Entities;
using CukConti.Domain.Enums;
using CukConti.Domain.Interfaces;

namespace CukConti.Application.Services
{
    public record ResultadoGeneracionAlertas(int ContratosEvaluados, int AlertasCreadas, int MailsEnviados);

    public class AlertaService
    {
        private readonly IContratoRepository _contratoRepository;
        private readonly IAlertaRepository _alertaRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEmailService _emailService;

        public AlertaService(
            IContratoRepository contratoRepository,
            IAlertaRepository alertaRepository,
            IUsuarioRepository usuarioRepository,
            IEmailService emailService)
        {
            _contratoRepository = contratoRepository;
            _alertaRepository = alertaRepository;
            _usuarioRepository = usuarioRepository;
            _emailService = emailService;
        }

        public async Task<ResultadoGeneracionAlertas> GenerarAlertasDiariasAsync()
        {
            var contratos = await _contratoRepository.ListarConDetalleAsync();
            var secretarios = (await _usuarioRepository.ListarSecretariosActivosAsync()).ToList();

            int alertasCreadas = 0;
            int mailsEnviados = 0;
            int contratosEvaluados = 0;
            var hoy = DateTime.UtcNow.Date;

            foreach (var contrato in contratos.Where(c => c.Estado != EstadoContrato.Finalizado))
            {
                contratosEvaluados++;
                var diasRestantes = (contrato.ProximaActualizacion.Date - hoy).Days;

                bool esUmbral60 = diasRestantes <= 60 && !contrato.Alerta60DiasEnviada;
                bool esUmbral30 = diasRestantes <= 30 && !contrato.Alerta30DiasEnviada;

                int? umbralAProcesar = esUmbral30 ? 30 : (esUmbral60 ? 60 : (int?)null);
                if (umbralAProcesar is null)
                    continue;

                var mensaje = $"El contrato de {contrato.Propiedad?.Direccion} (inquilino: {contrato.Inquilino?.Nombre} {contrato.Inquilino?.Apellido}) " +
                              $"vence su actualización en {diasRestantes} días ({contrato.ProximaActualizacion:dd/MM/yyyy}).";

                foreach (var secretario in secretarios)
                {
                    var enviado = await _emailService.EnviarAsync(secretario.Email, "Actualización de contrato próxima", mensaje);
                    if (enviado) mailsEnviados++;

                    var alerta = new Alerta
                    {
                        Tipo = TipoAlerta.ActualizacionContrato,
                        Mensaje = mensaje,
                        DiasAntelacion = umbralAProcesar.Value,
                        FechaGeneracion = DateTime.UtcNow,
                        EnviadaPorMail = enviado,
                        Leida = false,
                        UsuarioId = secretario.Id,
                        ContratoId = contrato.Id
                    };

                    await _alertaRepository.AgregarAsync(alerta);
                    alertasCreadas++;
                }

                if (!string.IsNullOrWhiteSpace(contrato.Inquilino?.Email))
                {
                    var mensajeInquilino = $"Te escribimos de Inmobiliaria Cuk Conti: tu contrato de alquiler en {contrato.Propiedad?.Direccion} " +
                                           $"va a actualizarse el {contrato.ProximaActualizacion:dd/MM/yyyy}. Cualquier consulta, contactanos.";
                    var enviadoInquilino = await _emailService.EnviarAsync(contrato.Inquilino.Email, "Próxima actualización de tu contrato de alquiler", mensajeInquilino);
                    if (enviadoInquilino) mailsEnviados++;
                }

                if (umbralAProcesar == 60) contrato.Alerta60DiasEnviada = true;
                if (umbralAProcesar == 30) contrato.Alerta30DiasEnviada = true;
                _contratoRepository.Actualizar(contrato);
            }

            await _contratoRepository.GuardarCambiosAsync();

            return new ResultadoGeneracionAlertas(contratosEvaluados, alertasCreadas, mailsEnviados);
        }
    }
}

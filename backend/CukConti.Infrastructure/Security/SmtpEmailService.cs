using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using CukConti.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CukConti.Infrastructure.Security
{
    public class SmtpEmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<SmtpEmailService> _logger;

        public SmtpEmailService(IConfiguration configuration, ILogger<SmtpEmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<bool> EnviarAsync(string destinatario, string asunto, string cuerpo)
        {
            try
            {
                var host = _configuration["Smtp:Host"]!;
                var port = int.Parse(_configuration["Smtp:Port"]!);
                var username = _configuration["Smtp:Username"]!;
                var password = _configuration["Smtp:Password"]!;
                var from = _configuration["Smtp:From"]!;

                using var client = new SmtpClient(host, port)
                {
                    Credentials = new NetworkCredential(username, password),
                    EnableSsl = true
                };

                using var mensaje = new MailMessage(from, destinatario, asunto, cuerpo);
                await client.SendMailAsync(mensaje);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al enviar mail a {Destinatario}", destinatario);
                return false;
            }
        }
    }
}

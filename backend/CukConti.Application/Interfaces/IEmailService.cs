using System.Threading.Tasks;

namespace CukConti.Application.Interfaces
{
    public interface IEmailService
    {
        Task<bool> EnviarAsync(string destinatario, string asunto, string cuerpo);
    }
}

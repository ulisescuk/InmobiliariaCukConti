namespace CukConti.Application.Interfaces
{
    public interface IReciboService
    {
        byte[] GenerarReciboPago(string propiedad, string inquilino, decimal monto, string periodo, System.DateTime fechaPago, string? metodoPago);
    }
}

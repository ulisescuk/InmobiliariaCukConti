namespace CukConti.Application.DTOs
{
    public record DashboardResponse(
        int ContratosProximosAVencerEsteMes,
        int PagosPendientes,
        decimal MontoPagosPendientes,
        decimal IngresosDelMes,
        decimal EgresosDelMes
    );
}

using System;
using CukConti.Application.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CukConti.Infrastructure.Security
{
    public class QuestPdfReciboService : IReciboService
    {
        public QuestPdfReciboService()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public byte[] GenerarReciboPago(string propiedad, string inquilino, decimal monto, string periodo, DateTime fechaPago, string? metodoPago)
        {
            var documento = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A5);
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    page.Header().Text("Inmobiliaria Cuk Conti").FontSize(18).Bold();

                    page.Content().PaddingTop(20).Column(col =>
                    {
                        col.Item().Text("Recibo de pago").FontSize(14).SemiBold();
                        col.Item().PaddingTop(10).Text($"Propiedad: {propiedad}");
                        col.Item().Text($"Inquilino: {inquilino}");
                        col.Item().Text($"Período: {periodo}");
                        col.Item().Text($"Fecha de pago: {fechaPago:dd/MM/yyyy}");
                        if (!string.IsNullOrWhiteSpace(metodoPago))
                            col.Item().Text($"Método de pago: {metodoPago}");
                        col.Item().PaddingTop(15).Text($"Monto: ${monto:N2}").FontSize(13).Bold();
                    });

                    page.Footer().AlignCenter().Text("Documento generado automáticamente por el sistema de administración.").FontSize(8);
                });
            });

            return documento.GeneratePdf();
        }
    }
}

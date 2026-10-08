using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CukConti.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregarIndicesDeRendimiento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ValoresIndice_IndiceId",
                table: "ValoresIndice");

            migrationBuilder.CreateIndex(
                name: "IX_ValoresIndice_IndiceId_Periodo",
                table: "ValoresIndice",
                columns: new[] { "IndiceId", "Periodo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Propietarios_Dni",
                table: "Propietarios",
                column: "Dni",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inquilinos_Dni",
                table: "Inquilinos",
                column: "Dni",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_Estado_ProximaActualizacion",
                table: "Contratos",
                columns: new[] { "Estado", "ProximaActualizacion" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ValoresIndice_IndiceId_Periodo",
                table: "ValoresIndice");

            migrationBuilder.DropIndex(
                name: "IX_Propietarios_Dni",
                table: "Propietarios");

            migrationBuilder.DropIndex(
                name: "IX_Inquilinos_Dni",
                table: "Inquilinos");

            migrationBuilder.DropIndex(
                name: "IX_Contratos_Estado_ProximaActualizacion",
                table: "Contratos");

            migrationBuilder.CreateIndex(
                name: "IX_ValoresIndice_IndiceId",
                table: "ValoresIndice",
                column: "IndiceId");
        }
    }
}

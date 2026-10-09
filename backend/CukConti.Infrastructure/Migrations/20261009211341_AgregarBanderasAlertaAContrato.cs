using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CukConti.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregarBanderasAlertaAContrato : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Alerta30DiasEnviada",
                table: "Contratos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Alerta60DiasEnviada",
                table: "Contratos",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alerta30DiasEnviada",
                table: "Contratos");

            migrationBuilder.DropColumn(
                name: "Alerta60DiasEnviada",
                table: "Contratos");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoMacacos.Data.Migrations
{
    /// <inheritdoc />
    public partial class bancoIsa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.AddColumn<int>(
                name: "Ano",
                table: "Veiculo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Cor",
                table: "Veiculo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Marca",
                table: "Veiculo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Modelo",
                table: "Veiculo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Placa",
                table: "Veiculo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataDevolucao",
                table: "Registros",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataRetirada",
                table: "Registros",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Registros",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "VeiculoId",
                table: "Registros",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Bairro",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CPF",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Celular",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DataNacimento",
                table: "Clientes",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Endereço",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Registros_VeiculoId",
                table: "Registros",
                column: "VeiculoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Registros_Veiculo_VeiculoId",
                table: "Registros",
                column: "VeiculoId",
                principalTable: "Veiculo",
                principalColumn: "VeiculoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registros_Veiculo_VeiculoId",
                table: "Registros");

            migrationBuilder.DropIndex(
                name: "IX_Registros_VeiculoId",
                table: "Registros");

            migrationBuilder.DropColumn(
                name: "Ano",
                table: "Veiculo");

            migrationBuilder.DropColumn(
                name: "Cor",
                table: "Veiculo");

            migrationBuilder.DropColumn(
                name: "Marca",
                table: "Veiculo");

            migrationBuilder.DropColumn(
                name: "Modelo",
                table: "Veiculo");

            migrationBuilder.DropColumn(
                name: "Placa",
                table: "Veiculo");

            migrationBuilder.DropColumn(
                name: "DataDevolucao",
                table: "Registros");

            migrationBuilder.DropColumn(
                name: "DataRetirada",
                table: "Registros");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Registros");

            migrationBuilder.DropColumn(
                name: "VeiculoId",
                table: "Registros");

            migrationBuilder.DropColumn(
                name: "Bairro",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "CPF",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Celular",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Cidade",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "DataNacimento",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Endereço",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Clientes");

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    StatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.StatusId);
                });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoMacacos.Data.Migrations
{
    /// <inheritdoc />
    public partial class final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Disponivel",
                table: "Veiculo",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Registros",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "VeiculoId1",
                table: "Registros",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Registros_VeiculoId1",
                table: "Registros",
                column: "VeiculoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Registros_Veiculo_VeiculoId1",
                table: "Registros",
                column: "VeiculoId1",
                principalTable: "Veiculo",
                principalColumn: "VeiculoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registros_Veiculo_VeiculoId1",
                table: "Registros");

            migrationBuilder.DropIndex(
                name: "IX_Registros_VeiculoId1",
                table: "Registros");

            migrationBuilder.DropColumn(
                name: "Disponivel",
                table: "Veiculo");

            migrationBuilder.DropColumn(
                name: "VeiculoId1",
                table: "Registros");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Registros",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}

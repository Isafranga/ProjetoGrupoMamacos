using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoMacacos.Data.Migrations
{
    /// <inheritdoc />
    public partial class alterTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ClienteId",
                table: "Registros",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Registros_ClienteId",
                table: "Registros",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Registros_Clientes_ClienteId",
                table: "Registros",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "ClienteId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registros_Clientes_ClienteId",
                table: "Registros");

            migrationBuilder.DropIndex(
                name: "IX_Registros_ClienteId",
                table: "Registros");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Registros");
        }
    }
}

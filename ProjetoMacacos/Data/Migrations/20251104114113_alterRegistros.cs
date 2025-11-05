using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoMacacos.Data.Migrations
{
    /// <inheritdoc />
    public partial class alterRegistros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Registros",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Registros",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Registros_UsuarioId",
                table: "Registros",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Registros_AspNetUsers_UsuarioId",
                table: "Registros",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registros_AspNetUsers_UsuarioId",
                table: "Registros");

            migrationBuilder.DropIndex(
                name: "IX_Registros_UsuarioId",
                table: "Registros");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Registros");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Registros");

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
    }
}

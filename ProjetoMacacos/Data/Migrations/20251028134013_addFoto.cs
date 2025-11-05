using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoMacacos.Data.Migrations
{
    /// <inheritdoc />
    public partial class addFoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlFoto",
                table: "Veiculo",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlFoto",
                table: "Veiculo");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiMediaDF.Migrations
{
    public partial class RelacionUsuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdUsuarioIdentity",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "username",
                table: "Comentarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdUsuarioIdentity",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "username",
                table: "Comentarios");
        }
    }
}

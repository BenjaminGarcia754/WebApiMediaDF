using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiMediaDF.Migrations
{
    public partial class añadirUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "CalificacionVideos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioRelacionado",
                table: "CalificacionVideos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CalificacionVideos_UsuarioId",
                table: "CalificacionVideos",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_CalificacionVideos_Usuarios_UsuarioId",
                table: "CalificacionVideos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalificacionVideos_Usuarios_UsuarioId",
                table: "CalificacionVideos");

            migrationBuilder.DropIndex(
                name: "IX_CalificacionVideos_UsuarioId",
                table: "CalificacionVideos");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "CalificacionVideos");

            migrationBuilder.DropColumn(
                name: "UsuarioRelacionado",
                table: "CalificacionVideos");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiMediaDF.Migrations
{
    public partial class CalificacionVideo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalificacionVideos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CalificacionUsuario = table.Column<int>(type: "int", nullable: false),
                    VideoRelacionado = table.Column<int>(type: "int", nullable: false),
                    VideoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalificacionVideos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalificacionVideos_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Videos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalificacionVideos_VideoId",
                table: "CalificacionVideos",
                column: "VideoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalificacionVideos");
        }
    }
}

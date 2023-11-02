using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiMediaDF.Migrations
{
    public partial class PrimeraMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Materias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ruta = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposReporte",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposReporte", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoUsuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoUsuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ruta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaSubida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Materia = table.Column<int>(type: "int", nullable: false),
                    Valoracion = table.Column<int>(type: "int", nullable: true),
                    MateriaNavigationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Videos_Materias_MateriaNavigationId",
                        column: x => x.MateriaNavigationId,
                        principalTable: "Materias",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Contraseña = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreDeUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoUsuarioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_TipoUsuarios_TipoUsuarioId",
                        column: x => x.TipoUsuarioId,
                        principalTable: "TipoUsuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comentarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VideoRelacionado = table.Column<int>(type: "int", nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoRelacionadoNavigationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comentarios_Videos_VideoRelacionadoNavigationId",
                        column: x => x.VideoRelacionadoNavigationId,
                        principalTable: "Videos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reportes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioReporte = table.Column<int>(type: "int", nullable: false),
                    VideoReporte = table.Column<int>(type: "int", nullable: false),
                    TipoReporte = table.Column<int>(type: "int", nullable: false),
                    UsuarioReporteNavigationId = table.Column<int>(type: "int", nullable: true),
                    VideoReporteNavigationId = table.Column<int>(type: "int", nullable: true),
                    TipoReporteNavigationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reportes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reportes_TiposReporte_TipoReporteNavigationId",
                        column: x => x.TipoReporteNavigationId,
                        principalTable: "TiposReporte",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reportes_Usuarios_UsuarioReporteNavigationId",
                        column: x => x.UsuarioReporteNavigationId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reportes_Videos_VideoReporteNavigationId",
                        column: x => x.VideoReporteNavigationId,
                        principalTable: "Videos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VideoTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdVideo = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: true),
                    VideoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoTypes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VideoTypes_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Videos",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "TipoUsuarios",
                columns: new[] { "Id", "Tipo" },
                values: new object[] { 1, "Administrador" });

            migrationBuilder.InsertData(
                table: "TipoUsuarios",
                columns: new[] { "Id", "Tipo" },
                values: new object[] { 2, "Usuario" });

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_VideoRelacionadoNavigationId",
                table: "Comentarios",
                column: "VideoRelacionadoNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reportes_TipoReporteNavigationId",
                table: "Reportes",
                column: "TipoReporteNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reportes_UsuarioReporteNavigationId",
                table: "Reportes",
                column: "UsuarioReporteNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reportes_VideoReporteNavigationId",
                table: "Reportes",
                column: "VideoReporteNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_TipoUsuarioId",
                table: "Usuarios",
                column: "TipoUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_MateriaNavigationId",
                table: "Videos",
                column: "MateriaNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoTypes_UsuarioId",
                table: "VideoTypes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoTypes_VideoId",
                table: "VideoTypes",
                column: "VideoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comentarios");

            migrationBuilder.DropTable(
                name: "Reportes");

            migrationBuilder.DropTable(
                name: "VideoTypes");

            migrationBuilder.DropTable(
                name: "TiposReporte");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropTable(
                name: "TipoUsuarios");

            migrationBuilder.DropTable(
                name: "Materias");
        }
    }
}

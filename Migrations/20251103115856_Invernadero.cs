using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invernadero.Migrations
{
    /// <inheritdoc />
    public partial class Invernadero : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalendarioRiego",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", maxLength: 9, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(22)", maxLength: 22, nullable: false),
                    NivelAgua = table.Column<int>(type: "int", maxLength: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarioRiego", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Registro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", maxLength: 9, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Temperatura = table.Column<decimal>(type: "decimal(18,2)", maxLength: 4, nullable: false),
                    Humedad = table.Column<decimal>(type: "decimal(18,2)", maxLength: 4, nullable: false),
                    Hora = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registro", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sensor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Ubicacion = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegistroSensor",
                columns: table => new
                {
                    IdRegistro = table.Column<int>(type: "int", nullable: false),
                    IdSensor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistroSensor", x => new { x.IdRegistro, x.IdSensor });
                    table.ForeignKey(
                        name: "FK_RegistroSensor_Registro_IdRegistro",
                        column: x => x.IdRegistro,
                        principalTable: "Registro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistroSensor_Sensor_IdSensor",
                        column: x => x.IdSensor,
                        principalTable: "Sensor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", maxLength: 9, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Contrasenia = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    SensorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Sensor_SensorId",
                        column: x => x.SensorId,
                        principalTable: "Sensor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notifica",
                columns: table => new
                {
                    IdCalendarioRiego = table.Column<int>(type: "int", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifica", x => x.IdCalendarioRiego);
                    table.ForeignKey(
                        name: "FK_Notifica_CalendarioRiego_IdCalendarioRiego",
                        column: x => x.IdCalendarioRiego,
                        principalTable: "CalendarioRiego",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifica_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegistroCalendario",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdCalendarioDeRiego = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistroCalendario", x => new { x.IdCalendarioDeRiego, x.IdUsuario });
                    table.ForeignKey(
                        name: "FK_RegistroCalendario_CalendarioRiego_IdCalendarioDeRiego",
                        column: x => x.IdCalendarioDeRiego,
                        principalTable: "CalendarioRiego",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistroCalendario_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegistroConfiguración",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdSensor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistroConfiguración", x => new { x.IdUsuario, x.IdSensor });
                    table.ForeignKey(
                        name: "FK_RegistroConfiguración_Sensor_IdSensor",
                        column: x => x.IdSensor,
                        principalTable: "Sensor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistroConfiguración_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifica_IdUsuario",
                table: "Notifica",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroCalendario_IdUsuario",
                table: "RegistroCalendario",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroConfiguración_IdSensor",
                table: "RegistroConfiguración",
                column: "IdSensor");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroSensor_IdSensor",
                table: "RegistroSensor",
                column: "IdSensor");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_SensorId",
                table: "Usuario",
                column: "SensorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifica");

            migrationBuilder.DropTable(
                name: "RegistroCalendario");

            migrationBuilder.DropTable(
                name: "RegistroConfiguración");

            migrationBuilder.DropTable(
                name: "RegistroSensor");

            migrationBuilder.DropTable(
                name: "CalendarioRiego");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Registro");

            migrationBuilder.DropTable(
                name: "Sensor");
        }
    }
}

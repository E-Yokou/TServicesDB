using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TServicesDB.Migrations.TServicesDB
{
    /// <inheritdoc />
    public partial class AddModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TSRoutes",
                columns: table => new
                {
                    TSRouteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numberRoute = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    firstCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    time = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TSRoutes", x => x.TSRouteID);
                });

            migrationBuilder.CreateTable(
                name: "TSBus",
                columns: table => new
                {
                    TSBusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    typebus = table.Column<string>(name: "type_bus", type: "nvarchar(max)", nullable: false),
                    brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    govermentnumber = table.Column<string>(name: "goverment_number", type: "nvarchar(max)", nullable: false),
                    place = table.Column<int>(type: "int", nullable: false),
                    TSRouteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TSBus", x => x.TSBusID);
                    table.ForeignKey(
                        name: "FK_TSBus_TSRoutes_TSRouteId",
                        column: x => x.TSRouteId,
                        principalTable: "TSRoutes",
                        principalColumn: "TSRouteID");
                });

            migrationBuilder.CreateTable(
                name: "TSDrivers",
                columns: table => new
                {
                    TSDriverID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    middleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TSBusId = table.Column<int>(type: "int", nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TSDrivers", x => x.TSDriverID);
                    table.ForeignKey(
                        name: "FK_TSDrivers_TSBus_TSBusId",
                        column: x => x.TSBusId,
                        principalTable: "TSBus",
                        principalColumn: "TSBusID");
                });

            migrationBuilder.CreateTable(
                name: "TSStopovers",
                columns: table => new
                {
                    TSStopoverID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TSRouteId = table.Column<int>(type: "int", nullable: true),
                    startcity = table.Column<string>(name: "start_city", type: "nvarchar(max)", nullable: false),
                    endcity = table.Column<string>(name: "end_city", type: "nvarchar(max)", nullable: false),
                    namestopover = table.Column<string>(name: "name_stopover", type: "nvarchar(max)", nullable: false),
                    price = table.Column<int>(type: "int", nullable: false),
                    TSBusId = table.Column<int>(type: "int", nullable: false),
                    time = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TSStopovers", x => x.TSStopoverID);
                    table.ForeignKey(
                        name: "FK_TSStopovers_TSBus_TSBusId",
                        column: x => x.TSBusId,
                        principalTable: "TSBus",
                        principalColumn: "TSBusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TSStopovers_TSRoutes_TSRouteId",
                        column: x => x.TSRouteId,
                        principalTable: "TSRoutes",
                        principalColumn: "TSRouteID");
                });

            migrationBuilder.CreateTable(
                name: "TSTickets",
                columns: table => new
                {
                    TSTicketID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numberroute = table.Column<string>(name: "number_route", type: "nvarchar(max)", nullable: false),
                    mailclient = table.Column<string>(name: "mail_client", type: "nvarchar(max)", nullable: false),
                    stopover = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    datesale = table.Column<DateTime>(name: "date_sale", type: "datetime2", nullable: false),
                    dateroute = table.Column<string>(name: "date_route", type: "nvarchar(max)", nullable: false),
                    price = table.Column<int>(type: "int", nullable: false),
                    TSBusSecondId = table.Column<int>(type: "int", nullable: false),
                    TSBusID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TSTickets", x => x.TSTicketID);
                    table.ForeignKey(
                        name: "FK_TSTickets_TSBus_TSBusID",
                        column: x => x.TSBusID,
                        principalTable: "TSBus",
                        principalColumn: "TSBusID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TSBus_TSRouteId",
                table: "TSBus",
                column: "TSRouteId");

            migrationBuilder.CreateIndex(
                name: "IX_TSDrivers_TSBusId",
                table: "TSDrivers",
                column: "TSBusId");

            migrationBuilder.CreateIndex(
                name: "IX_TSStopovers_TSBusId",
                table: "TSStopovers",
                column: "TSBusId");

            migrationBuilder.CreateIndex(
                name: "IX_TSStopovers_TSRouteId",
                table: "TSStopovers",
                column: "TSRouteId");

            migrationBuilder.CreateIndex(
                name: "IX_TSTickets_TSBusID",
                table: "TSTickets",
                column: "TSBusID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TSDrivers");

            migrationBuilder.DropTable(
                name: "TSStopovers");

            migrationBuilder.DropTable(
                name: "TSTickets");

            migrationBuilder.DropTable(
                name: "TSBus");

            migrationBuilder.DropTable(
                name: "TSRoutes");
        }
    }
}

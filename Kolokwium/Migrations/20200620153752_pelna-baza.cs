using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kolokwium.Migrations
{
    public partial class pelnabaza : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artists",
                columns: table => new
                {
                    idArtist = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nickname = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.idArtist);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    idEvent = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(nullable: true),
                    startDate = table.Column<DateTime>(nullable: false),
                    endDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.idEvent);
                });

            migrationBuilder.CreateTable(
                name: "Organisers",
                columns: table => new
                {
                    idOrganiser = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organisers", x => x.idOrganiser);
                });

            migrationBuilder.CreateTable(
                name: "Artist_Events",
                columns: table => new
                {
                    idArtistEvent = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idArtist = table.Column<int>(nullable: false),
                    idEvent = table.Column<int>(nullable: false),
                    artistidArtist = table.Column<int>(nullable: true),
                    PerformanceDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artist_Events", x => x.idArtistEvent);
                    table.ForeignKey(
                        name: "FK_Artist_Events_Artists_artistidArtist",
                        column: x => x.artistidArtist,
                        principalTable: "Artists",
                        principalColumn: "idArtist",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Artist_Events_Events_idEvent",
                        column: x => x.idEvent,
                        principalTable: "Events",
                        principalColumn: "idEvent",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Event_Organisers",
                columns: table => new
                {
                    idEventOrganiser = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idOrganiser = table.Column<int>(nullable: false),
                    idEvent = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event_Organisers", x => x.idEventOrganiser);
                    table.ForeignKey(
                        name: "FK_Event_Organisers_Events_idEvent",
                        column: x => x.idEvent,
                        principalTable: "Events",
                        principalColumn: "idEvent",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Event_Organisers_Organisers_idOrganiser",
                        column: x => x.idOrganiser,
                        principalTable: "Organisers",
                        principalColumn: "idOrganiser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artist_Events_artistidArtist",
                table: "Artist_Events",
                column: "artistidArtist");

            migrationBuilder.CreateIndex(
                name: "IX_Artist_Events_idEvent",
                table: "Artist_Events",
                column: "idEvent");

            migrationBuilder.CreateIndex(
                name: "IX_Event_Organisers_idEvent",
                table: "Event_Organisers",
                column: "idEvent");

            migrationBuilder.CreateIndex(
                name: "IX_Event_Organisers_idOrganiser",
                table: "Event_Organisers",
                column: "idOrganiser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Artist_Events");

            migrationBuilder.DropTable(
                name: "Event_Organisers");

            migrationBuilder.DropTable(
                name: "Artists");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Organisers");
        }
    }
}

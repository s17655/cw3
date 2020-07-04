using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Test1.Migrations
{
    public partial class DodatkoweKlasy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArtistEvents",
                columns: table => new
                {
                    idArtist = table.Column<int>(nullable: false),
                    idEvent = table.Column<int>(nullable: false),
                    performanceDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistEvents", x => new { x.idArtist, x.idEvent });
                });

            migrationBuilder.CreateTable(
                name: "EventOrganisers",
                columns: table => new
                {
                    idEvent = table.Column<int>(nullable: false),
                    idOrganiser = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventOrganisers", x => new { x.idEvent, x.idOrganiser });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtistEvents");

            migrationBuilder.DropTable(
                name: "EventOrganisers");
        }
    }
}

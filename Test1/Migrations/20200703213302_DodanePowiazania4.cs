using Microsoft.EntityFrameworkCore.Migrations;

namespace Test1.Migrations
{
    public partial class DodanePowiazania4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_EventOrganisers_idOrganiser",
                table: "EventOrganisers",
                column: "idOrganiser");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistEvents_idEvent",
                table: "ArtistEvents",
                column: "idEvent");

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistEvents_Artists_idArtist",
                table: "ArtistEvents",
                column: "idArtist",
                principalTable: "Artists",
                principalColumn: "idArtist",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistEvents_Events_idEvent",
                table: "ArtistEvents",
                column: "idEvent",
                principalTable: "Events",
                principalColumn: "idEvent",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventOrganisers_Events_idEvent",
                table: "EventOrganisers",
                column: "idEvent",
                principalTable: "Events",
                principalColumn: "idEvent",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventOrganisers_Organisers_idOrganiser",
                table: "EventOrganisers",
                column: "idOrganiser",
                principalTable: "Organisers",
                principalColumn: "idOrganiser",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArtistEvents_Artists_idArtist",
                table: "ArtistEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistEvents_Events_idEvent",
                table: "ArtistEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_EventOrganisers_Events_idEvent",
                table: "EventOrganisers");

            migrationBuilder.DropForeignKey(
                name: "FK_EventOrganisers_Organisers_idOrganiser",
                table: "EventOrganisers");

            migrationBuilder.DropIndex(
                name: "IX_EventOrganisers_idOrganiser",
                table: "EventOrganisers");

            migrationBuilder.DropIndex(
                name: "IX_ArtistEvents_idEvent",
                table: "ArtistEvents");
        }
    }
}

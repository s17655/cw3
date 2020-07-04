using Microsoft.EntityFrameworkCore.Migrations;

namespace Kolokwium2.Migrations
{
    public partial class powiazawniaFireAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FirefighterAction",
                columns: table => new
                {
                    idFirefighter = table.Column<int>(nullable: false),
                    idAction = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirefighterAction", x => new { x.idAction, x.idFirefighter });
                    table.ForeignKey(
                        name: "FK_FirefighterAction_Actions_idAction",
                        column: x => x.idAction,
                        principalTable: "Actions",
                        principalColumn: "idAction",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FirefighterAction_Firefighters_idFirefighter",
                        column: x => x.idFirefighter,
                        principalTable: "Firefighters",
                        principalColumn: "idFirefighter",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FirefighterAction_idFirefighter",
                table: "FirefighterAction",
                column: "idFirefighter");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FirefighterAction");
        }
    }
}

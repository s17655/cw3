using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kolokwium2.Migrations
{
    public partial class powiazawniaPozostale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FirefighterAction_Actions_idAction",
                table: "FirefighterAction");

            migrationBuilder.DropForeignKey(
                name: "FK_FirefighterAction_Firefighters_idFirefighter",
                table: "FirefighterAction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FirefighterAction",
                table: "FirefighterAction");

            migrationBuilder.RenameTable(
                name: "FirefighterAction",
                newName: "FirefighterActions");

            migrationBuilder.RenameIndex(
                name: "IX_FirefighterAction_idFirefighter",
                table: "FirefighterActions",
                newName: "IX_FirefighterActions_idFirefighter");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FirefighterActions",
                table: "FirefighterActions",
                columns: new[] { "idAction", "idFirefighter" });

            migrationBuilder.CreateTable(
                name: "FireTruckActions",
                columns: table => new
                {
                    idFireTruckAction = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idFireTruck = table.Column<int>(nullable: false),
                    idAction = table.Column<int>(nullable: false),
                    assignmentDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireTruckActions", x => x.idFireTruckAction);
                    table.ForeignKey(
                        name: "FK_FireTruckActions_Actions_idAction",
                        column: x => x.idAction,
                        principalTable: "Actions",
                        principalColumn: "idAction",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FireTruckActions_FireTrucks_idFireTruck",
                        column: x => x.idFireTruck,
                        principalTable: "FireTrucks",
                        principalColumn: "idFireTruck",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FireTruckActions_idAction",
                table: "FireTruckActions",
                column: "idAction");

            migrationBuilder.CreateIndex(
                name: "IX_FireTruckActions_idFireTruck",
                table: "FireTruckActions",
                column: "idFireTruck");

            migrationBuilder.AddForeignKey(
                name: "FK_FirefighterActions_Actions_idAction",
                table: "FirefighterActions",
                column: "idAction",
                principalTable: "Actions",
                principalColumn: "idAction",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FirefighterActions_Firefighters_idFirefighter",
                table: "FirefighterActions",
                column: "idFirefighter",
                principalTable: "Firefighters",
                principalColumn: "idFirefighter",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FirefighterActions_Actions_idAction",
                table: "FirefighterActions");

            migrationBuilder.DropForeignKey(
                name: "FK_FirefighterActions_Firefighters_idFirefighter",
                table: "FirefighterActions");

            migrationBuilder.DropTable(
                name: "FireTruckActions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FirefighterActions",
                table: "FirefighterActions");

            migrationBuilder.RenameTable(
                name: "FirefighterActions",
                newName: "FirefighterAction");

            migrationBuilder.RenameIndex(
                name: "IX_FirefighterActions_idFirefighter",
                table: "FirefighterAction",
                newName: "IX_FirefighterAction_idFirefighter");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FirefighterAction",
                table: "FirefighterAction",
                columns: new[] { "idAction", "idFirefighter" });

            migrationBuilder.AddForeignKey(
                name: "FK_FirefighterAction_Actions_idAction",
                table: "FirefighterAction",
                column: "idAction",
                principalTable: "Actions",
                principalColumn: "idAction",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FirefighterAction_Firefighters_idFirefighter",
                table: "FirefighterAction",
                column: "idFirefighter",
                principalTable: "Firefighters",
                principalColumn: "idFirefighter",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

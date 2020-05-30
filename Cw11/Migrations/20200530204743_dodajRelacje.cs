using Microsoft.EntityFrameworkCore.Migrations;

namespace Cw11.Migrations
{
    public partial class dodajRelacje : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "doctoridDoctor",
                table: "Prescriptions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "patientidPatient",
                table: "Prescriptions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "idMedicament",
                table: "Prescription_Medicaments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "idPrescription",
                table: "Prescription_Medicaments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_doctoridDoctor",
                table: "Prescriptions",
                column: "doctoridDoctor");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_patientidPatient",
                table: "Prescriptions",
                column: "patientidPatient");

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_Medicaments_idMedicament",
                table: "Prescription_Medicaments",
                column: "idMedicament");

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_Medicaments_idPrescription",
                table: "Prescription_Medicaments",
                column: "idPrescription");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Medicaments_Medicaments_idMedicament",
                table: "Prescription_Medicaments",
                column: "idMedicament",
                principalTable: "Medicaments",
                principalColumn: "idMedicament",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Medicaments_Prescriptions_idPrescription",
                table: "Prescription_Medicaments",
                column: "idPrescription",
                principalTable: "Prescriptions",
                principalColumn: "idPrescription",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Doctors_doctoridDoctor",
                table: "Prescriptions",
                column: "doctoridDoctor",
                principalTable: "Doctors",
                principalColumn: "idDoctor",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Patients_patientidPatient",
                table: "Prescriptions",
                column: "patientidPatient",
                principalTable: "Patients",
                principalColumn: "idPatient",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Medicaments_Medicaments_idMedicament",
                table: "Prescription_Medicaments");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Medicaments_Prescriptions_idPrescription",
                table: "Prescription_Medicaments");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Doctors_doctoridDoctor",
                table: "Prescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Patients_patientidPatient",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_doctoridDoctor",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_patientidPatient",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Prescription_Medicaments_idMedicament",
                table: "Prescription_Medicaments");

            migrationBuilder.DropIndex(
                name: "IX_Prescription_Medicaments_idPrescription",
                table: "Prescription_Medicaments");

            migrationBuilder.DropColumn(
                name: "doctoridDoctor",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "patientidPatient",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "idMedicament",
                table: "Prescription_Medicaments");

            migrationBuilder.DropColumn(
                name: "idPrescription",
                table: "Prescription_Medicaments");
        }
    }
}

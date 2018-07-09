using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace P01_HospitalDatabase.Data.Migrations
{
    public partial class AddDoctors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientsMedicaments_Medicaments_MedicamentId1",
                table: "PatientsMedicaments");

            migrationBuilder.DropIndex(
                name: "IX_PatientsMedicaments_MedicamentId1",
                table: "PatientsMedicaments");

            migrationBuilder.DropColumn(
                name: "MedicamentId1",
                table: "PatientsMedicaments");

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "Visitations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Patients",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Patients",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Patients",
                unicode: false,
                maxLength: 80,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Patients",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MedicamentId",
                table: "Medicaments",
                nullable: false,
                oldClrType: typeof(string))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    DoctorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Specialty = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.DoctorId);
                });

            migrationBuilder.InsertData(
                table: "Medicaments",
                columns: new[] { "MedicamentId", "Name" },
                values: new object[,]
                {
                    { 1, "Biseptol" },
                    { 18, "Terramicina Oftalmica" },
                    { 17, "Reglin" },
                    { 16, "Propoven" },
                    { 15, "Primperan" },
                    { 14, "Primolut Nor" },
                    { 13, "Pentrexyl" },
                    { 12, "Olfen" },
                    { 11, "Nistatin" },
                    { 10, "Navidoxine" },
                    { 9, "Fluimucil" },
                    { 8, "Flanax" },
                    { 7, "Efedrin" },
                    { 6, "Duvadilan" },
                    { 5, "Disflatyl" },
                    { 4, "Diclofenaco" },
                    { 3, "Curam" },
                    { 2, "Ciclobenzaprina" },
                    { 19, "Ultran" },
                    { 20, "Viartril-S" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Visitations_DoctorId",
                table: "Visitations",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientsMedicaments_Medicaments_MedicamentId",
                table: "PatientsMedicaments",
                column: "MedicamentId",
                principalTable: "Medicaments",
                principalColumn: "MedicamentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visitations_Doctors_DoctorId",
                table: "Visitations",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientsMedicaments_Medicaments_MedicamentId",
                table: "PatientsMedicaments");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitations_Doctors_DoctorId",
                table: "Visitations");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Visitations_DoctorId",
                table: "Visitations");

            migrationBuilder.DeleteData(
                table: "Medicaments",
                keyColumn: "MedicamentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Medicaments",
                keyColumn: "MedicamentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Medicaments",
                keyColumn: "MedicamentId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Medicaments",
                keyColumn: "MedicamentId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Medicaments",
                keyColumn: "MedicamentId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Medicaments",
                keyColumn: "MedicamentId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Medicaments",
                keyColumn: "MedicamentId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Medicaments",
                keyColumn: "MedicamentId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Medicaments",
                keyColumn: "MedicamentId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Medicaments",
                keyColumn: "MedicamentId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Medicaments",
                keyColumn: "MedicamentId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Medicaments",
                keyColumn: "MedicamentId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Medicaments",
                keyColumn: "MedicamentId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Medicaments",
                keyColumn: "MedicamentId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Medicaments",
                keyColumn: "MedicamentId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Medicaments",
                keyColumn: "MedicamentId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Medicaments",
                keyColumn: "MedicamentId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Medicaments",
                keyColumn: "MedicamentId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Medicaments",
                keyColumn: "MedicamentId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Medicaments",
                keyColumn: "MedicamentId",
                keyValue: 20);

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Visitations");

            migrationBuilder.AddColumn<string>(
                name: "MedicamentId1",
                table: "PatientsMedicaments",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Patients",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Patients",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Patients",
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 80,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Patients",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MedicamentId",
                table: "Medicaments",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.CreateIndex(
                name: "IX_PatientsMedicaments_MedicamentId1",
                table: "PatientsMedicaments",
                column: "MedicamentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientsMedicaments_Medicaments_MedicamentId1",
                table: "PatientsMedicaments",
                column: "MedicamentId1",
                principalTable: "Medicaments",
                principalColumn: "MedicamentId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

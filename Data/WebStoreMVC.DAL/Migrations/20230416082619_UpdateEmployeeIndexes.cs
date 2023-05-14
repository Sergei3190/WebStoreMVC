using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebStoreMVC.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEmployeeIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Employees_FirstName_LastName_MiddleName_Age",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_LastName_FirstName_MiddleName_Age",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_MiddleName_LastName_FirstName_Age",
                table: "Employees");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_FirstName_LastName_MiddleName_Age",
                table: "Employees",
                columns: new[] { "FirstName", "LastName", "MiddleName", "Age" },
                unique: true,
                filter: "[MiddleName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_LastName_FirstName_MiddleName_Age",
                table: "Employees",
                columns: new[] { "LastName", "FirstName", "MiddleName", "Age" },
                unique: true,
                filter: "[MiddleName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_MiddleName_LastName_FirstName_Age",
                table: "Employees",
                columns: new[] { "MiddleName", "LastName", "FirstName", "Age" },
                unique: true,
                filter: "[MiddleName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Employees_FirstName_LastName_MiddleName_Age",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_LastName_FirstName_MiddleName_Age",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_MiddleName_LastName_FirstName_Age",
                table: "Employees");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_FirstName_LastName_MiddleName_Age",
                table: "Employees",
                columns: new[] { "FirstName", "LastName", "MiddleName", "Age" });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_LastName_FirstName_MiddleName_Age",
                table: "Employees",
                columns: new[] { "LastName", "FirstName", "MiddleName", "Age" });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_MiddleName_LastName_FirstName_Age",
                table: "Employees",
                columns: new[] { "MiddleName", "LastName", "FirstName", "Age" });
        }
    }
}

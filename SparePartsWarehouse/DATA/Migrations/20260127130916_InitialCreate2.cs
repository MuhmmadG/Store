using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SparePartsWarehouse.DATA.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FactoryDepartments",
                columns: new[] { "Id", "IsActive", "Name" },
                values: new object[,]
                {
                    { 6, true, "كومبروسور" },
                    { 7, true, "الغلايه" }
                });

            migrationBuilder.InsertData(
                table: "Machines",
                columns: new[] { "Id", "FactoryDepartmentId", "MachineName" },
                values: new object[,]
                {
                    { 33, 6, "كومبروسور الكبير" },
                    { 34, 6, "كومبروسور الصغير" },
                    { 35, 6, "كومبروسور كيذر" },
                    { 36, 6, "كومبروسور أطلس" },
                    { 37, 7, "الغلايه القديمه" },
                    { 38, 7, "الغلايه الجديده" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Machines",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Machines",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Machines",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Machines",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Machines",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Machines",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "FactoryDepartments",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "FactoryDepartments",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}

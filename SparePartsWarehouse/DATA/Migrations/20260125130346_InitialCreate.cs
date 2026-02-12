using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SparePartsWarehouse.DATA.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentEditsLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DocumentId = table.Column<int>(type: "int", nullable: false),
                    FieldName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OldValue = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NewValue = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EditedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EditedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentEditsLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FactoryDepartments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactoryDepartments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MachineSparePartsCost",
                columns: table => new
                {
                    FactoryDepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MachineName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "MachineSparePartsCosts",
                columns: table => new
                {
                    FactoryDepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MachineName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "StoreDepartment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreDepartment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Machines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachineName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    FactoryDepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Machines_FactoryDepartments_FactoryDepartmentId",
                        column: x => x.FactoryDepartmentId,
                        principalTable: "FactoryDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    StoreDepartmentId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_StoreDepartment_StoreDepartmentId",
                        column: x => x.StoreDepartmentId,
                        principalTable: "StoreDepartment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseIns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberInvoice = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    PurchasedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ReceivedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseIns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseIns_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StockOuts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IssuerName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true, defaultValue: "Ahmed"),
                    ReceiverName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    StoreDepartmentId = table.Column<int>(type: "int", nullable: false),
                    FactoryDepartmentId = table.Column<int>(type: "int", nullable: false),
                    MachineId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockOuts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockOuts_FactoryDepartments_FactoryDepartmentId",
                        column: x => x.FactoryDepartmentId,
                        principalTable: "FactoryDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockOuts_Machines_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_StockOuts_StoreDepartment_StoreDepartmentId",
                        column: x => x.StoreDepartmentId,
                        principalTable: "StoreDepartment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Specification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReorderLevel = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemDescriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemDescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemDescriptions_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseInDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseInId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    ItemDescriptionId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AverageUnitCost = table.Column<decimal>(type: "decimal(8,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseInDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseInDetails_ItemDescriptions_ItemDescriptionId",
                        column: x => x.ItemDescriptionId,
                        principalTable: "ItemDescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseInDetails_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseInDetails_PurchaseIns_PurchaseInId",
                        column: x => x.PurchaseInId,
                        principalTable: "PurchaseIns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockAdjustments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemDescriptionId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferenceType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferenceId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockAdjustments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockAdjustments_ItemDescriptions_ItemDescriptionId",
                        column: x => x.ItemDescriptionId,
                        principalTable: "ItemDescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockOutDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StockOutId = table.Column<int>(type: "int", nullable: false),
                    ItemDescriptionId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    AverageUnitCost = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    ItemId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockOutDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockOutDetails_ItemDescriptions_ItemDescriptionId",
                        column: x => x.ItemDescriptionId,
                        principalTable: "ItemDescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockOutDetails_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockOutDetails_Items_ItemId1",
                        column: x => x.ItemId1,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StockOutDetails_StockOuts_StockOutId",
                        column: x => x.StockOutId,
                        principalTable: "StockOuts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "FactoryDepartments",
                columns: new[] { "Id", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, true, "الجلد" },
                    { 2, true, "النسيج" },
                    { 3, true, "قطع غيار" },
                    { 4, true, "الاداره" },
                    { 5, true, "مخزن" }
                });

            migrationBuilder.InsertData(
                table: "StoreDepartment",
                columns: new[] { "Id", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, true, "ميكانيكا" },
                    { 2, true, "كهرباء" },
                    { 3, true, "عدد وقطع تشغيل" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "IsActive", "Name", "StoreDepartmentId" },
                values: new object[,]
                {
                    { 1, true, "سيور", 1 },
                    { 2, true, "رولمان بلي", 1 },
                    { 3, true, "تروس", 1 },
                    { 4, true, "مواتير", 2 },
                    { 5, true, "لمبات", 2 }
                });

            migrationBuilder.InsertData(
                table: "Machines",
                columns: new[] { "Id", "FactoryDepartmentId", "MachineName" },
                values: new object[,]
                {
                    { 1, 1, "الخط القديم" },
                    { 2, 1, "الخط الجديد" },
                    { 3, 1, "خلاط" },
                    { 4, 1, "طباعه" },
                    { 5, 1, "امبوز" },
                    { 6, 1, "كاردا" },
                    { 7, 1, "مقص" },
                    { 8, 1, "اللفاف" },
                    { 9, 1, "الفرز" },
                    { 10, 1, "مرافق" },
                    { 11, 1, "كلارك" },
                    { 12, 1, "اسانسير" },
                    { 13, 2, "ماكينه 1 اورزيو" },
                    { 14, 2, "ماكينه 2 اورزيو" },
                    { 15, 2, "ماكينه 3 اورزيو" },
                    { 16, 2, "ماكينه 4 اورزيو" },
                    { 17, 2, "ماكينه 5 باى لونج" },
                    { 18, 2, "ماكينه 6 سانج يونج" },
                    { 19, 2, "ماكينه 7 الجديده" },
                    { 20, 2, "ماكينه 8 " },
                    { 21, 2, "ماكينه 9   جاكار " },
                    { 22, 2, "ماكينه 10 فرو" },
                    { 23, 2, "ماكينه 11 فرو" },
                    { 24, 2, "ماكينه 12 فرو" },
                    { 25, 2, "ماكينه 13 فرو" },
                    { 26, 2, "ماكينه 14 فرو" },
                    { 27, 2, "ماكينه 15 بو ليستر لامع" },
                    { 28, 2, "ماكينه 16 فرو أكرليليك" },
                    { 29, 3, "عدد و ادوات" },
                    { 30, 4, "مرافق" },
                    { 31, 5, "اخرى" },
                    { 32, 5, "مرافق" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_StoreDepartmentId",
                table: "Categories",
                column: "StoreDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_FactoryDepartments_Name",
                table: "FactoryDepartments",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemDescriptions_ItemId_ItemCode",
                table: "ItemDescriptions",
                columns: new[] { "ItemId", "ItemCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_CategoryId",
                table: "Items",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemName_CategoryId",
                table: "Items",
                columns: new[] { "ItemName", "CategoryId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Machines_FactoryDepartmentId",
                table: "Machines",
                column: "FactoryDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Machines_MachineName_FactoryDepartmentId",
                table: "Machines",
                columns: new[] { "MachineName", "FactoryDepartmentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseInDetails_ItemDescriptionId",
                table: "PurchaseInDetails",
                column: "ItemDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseInDetails_ItemId",
                table: "PurchaseInDetails",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseInDetails_PurchaseInId",
                table: "PurchaseInDetails",
                column: "PurchaseInId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseIns_SupplierId",
                table: "PurchaseIns",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_StockAdjustments_ItemDescriptionId",
                table: "StockAdjustments",
                column: "ItemDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_StockOutDetails_ItemDescriptionId",
                table: "StockOutDetails",
                column: "ItemDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_StockOutDetails_ItemId",
                table: "StockOutDetails",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_StockOutDetails_ItemId1",
                table: "StockOutDetails",
                column: "ItemId1");

            migrationBuilder.CreateIndex(
                name: "IX_StockOutDetails_StockOutId_ItemId_ItemDescriptionId",
                table: "StockOutDetails",
                columns: new[] { "StockOutId", "ItemId", "ItemDescriptionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockOuts_FactoryDepartmentId",
                table: "StockOuts",
                column: "FactoryDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_StockOuts_MachineId",
                table: "StockOuts",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_StockOuts_StoreDepartmentId",
                table: "StockOuts",
                column: "StoreDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreDepartment_Name",
                table: "StoreDepartment",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_SupplierName",
                table: "Suppliers",
                column: "SupplierName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentEditsLogs");

            migrationBuilder.DropTable(
                name: "MachineSparePartsCost");

            migrationBuilder.DropTable(
                name: "MachineSparePartsCosts");

            migrationBuilder.DropTable(
                name: "PurchaseInDetails");

            migrationBuilder.DropTable(
                name: "StockAdjustments");

            migrationBuilder.DropTable(
                name: "StockOutDetails");

            migrationBuilder.DropTable(
                name: "PurchaseIns");

            migrationBuilder.DropTable(
                name: "ItemDescriptions");

            migrationBuilder.DropTable(
                name: "StockOuts");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Machines");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "FactoryDepartments");

            migrationBuilder.DropTable(
                name: "StoreDepartment");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopOnline.Data.Migrations
{
    public partial class addtableimage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 3, 27, 11, 34, 4, 562, DateTimeKind.Local).AddTicks(4460),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 3, 23, 15, 36, 22, 829, DateTimeKind.Local).AddTicks(7093));

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    IsDefaul = table.Column<bool>(type: "bit", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SortOder = table.Column<int>(type: "int", nullable: false),
                    FileSize = table.Column<int>(type: "int", nullable: false),
                    ProdutId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProdutId",
                        column: x => x.ProdutId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "17d830f9-a1cc-4588-8930-81c12751a751");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8ffdc2b8-a060-45d6-a17a-77e3c7ebc34c", "AQAAAAEAACcQAAAAEMdx/nl9AE92ps/Hte0eLlDIO75SmD/KDEiLu8/TUAlK9cul+brGbtmMMzHq03Y3hg==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 3, 27, 11, 34, 4, 567, DateTimeKind.Local).AddTicks(7333));

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProdutId",
                table: "ProductImages",
                column: "ProdutId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 3, 23, 15, 36, 22, 829, DateTimeKind.Local).AddTicks(7093),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 3, 27, 11, 34, 4, 562, DateTimeKind.Local).AddTicks(4460));

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "70acd581-de83-4904-b828-ba6003adc7bb");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1821d2df-0fe6-4616-8747-b4a72feb2e97", "AQAAAAEAACcQAAAAEGwDrEtnLXA3h0gGvE6oQDpmS3Cv1zoMyQVWabzpCLQzauakOp4TidcgTpaReyDu0Q==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 3, 23, 15, 36, 22, 832, DateTimeKind.Local).AddTicks(7423));
        }
    }
}

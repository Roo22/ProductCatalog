using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication5.Migrations
{
    public partial class userid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "StartDate", "UserId" },
                values: new object[] { new DateTime(2023, 7, 23, 21, 48, 34, 381, DateTimeKind.Local).AddTicks(8849), new DateTime(2023, 7, 24, 21, 48, 34, 391, DateTimeKind.Local).AddTicks(7461), 1 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "StartDate", "UserId" },
                values: new object[] { new DateTime(2023, 7, 23, 21, 48, 34, 391, DateTimeKind.Local).AddTicks(8578), new DateTime(2023, 7, 25, 21, 48, 34, 391, DateTimeKind.Local).AddTicks(8606), 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "StartDate" },
                values: new object[] { new DateTime(2023, 7, 23, 20, 55, 10, 7, DateTimeKind.Local).AddTicks(2059), new DateTime(2023, 7, 24, 20, 55, 10, 10, DateTimeKind.Local).AddTicks(454) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "StartDate" },
                values: new object[] { new DateTime(2023, 7, 23, 20, 55, 10, 10, DateTimeKind.Local).AddTicks(1110), new DateTime(2023, 7, 25, 20, 55, 10, 10, DateTimeKind.Local).AddTicks(1138) });
        }
    }
}

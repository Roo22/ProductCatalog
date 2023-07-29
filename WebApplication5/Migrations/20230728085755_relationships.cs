using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication5.Migrations
{
    public partial class relationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "StartDate" },
                values: new object[] { new DateTime(2023, 7, 28, 11, 57, 54, 760, DateTimeKind.Local).AddTicks(4236), new DateTime(2023, 7, 29, 11, 57, 54, 763, DateTimeKind.Local).AddTicks(7291) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "StartDate" },
                values: new object[] { new DateTime(2023, 7, 28, 11, 57, 54, 763, DateTimeKind.Local).AddTicks(8023), new DateTime(2023, 7, 30, 11, 57, 54, 763, DateTimeKind.Local).AddTicks(8039) });

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserId1",
                table: "Products",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_UserId1",
                table: "Products",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_UserId1",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_UserId1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "StartDate" },
                values: new object[] { new DateTime(2023, 7, 27, 12, 1, 3, 925, DateTimeKind.Local).AddTicks(3540), new DateTime(2023, 7, 28, 12, 1, 3, 931, DateTimeKind.Local).AddTicks(4171) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "StartDate" },
                values: new object[] { new DateTime(2023, 7, 27, 12, 1, 3, 931, DateTimeKind.Local).AddTicks(5760), new DateTime(2023, 7, 29, 12, 1, 3, 931, DateTimeKind.Local).AddTicks(5794) });
        }
    }
}

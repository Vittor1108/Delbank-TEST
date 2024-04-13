using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Delbank.Infra.Data.SQL.Migrations
{
    /// <inheritdoc />
    public partial class corretciondefaultvalue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "DVD",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 12, 22, 1, 20, 164, DateTimeKind.Local).AddTicks(5970),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 10, 22, 21, 37, 457, DateTimeKind.Local).AddTicks(5990));

            migrationBuilder.AlterColumn<bool>(
                name: "Avaliable",
                table: "DVD",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                table: "DVD",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Director",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 12, 22, 1, 20, 162, DateTimeKind.Local).AddTicks(6663),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 10, 22, 21, 37, 455, DateTimeKind.Local).AddTicks(606));

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                table: "Director",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "DVD",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 10, 22, 21, 37, 457, DateTimeKind.Local).AddTicks(5990),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 12, 22, 1, 20, 164, DateTimeKind.Local).AddTicks(5970));

            migrationBuilder.AlterColumn<bool>(
                name: "Avaliable",
                table: "DVD",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                table: "DVD",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Director",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 10, 22, 21, 37, 455, DateTimeKind.Local).AddTicks(606),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 12, 22, 1, 20, 162, DateTimeKind.Local).AddTicks(6663));

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                table: "Director",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);
        }
    }
}

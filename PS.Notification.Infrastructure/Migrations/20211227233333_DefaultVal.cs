using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PS.Notification.Infrastructure.Migrations
{
    public partial class DefaultVal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsSend",
                schema: "PSN",
                table: "MsgMails",
                newName: "IsSent");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "PSN",
                table: "MsgMails",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2021, 12, 27, 11, 13, 59, 381, DateTimeKind.Local).AddTicks(6417));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsSent",
                schema: "PSN",
                table: "MsgMails",
                newName: "IsSend");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "PSN",
                table: "MsgMails",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 27, 11, 13, 59, 381, DateTimeKind.Local).AddTicks(6417),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");
        }
    }
}

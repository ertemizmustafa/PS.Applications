using Microsoft.EntityFrameworkCore.Migrations;

namespace PS.Notification.Infrastructure.Migrations
{
    public partial class MsgChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cc",
                schema: "PSN",
                table: "MsgMails");

            migrationBuilder.RenameColumn(
                name: "To",
                schema: "PSN",
                table: "MsgMails",
                newName: "ToRecipients");

            migrationBuilder.RenameColumn(
                name: "SendTime",
                schema: "PSN",
                table: "MsgMails",
                newName: "SentTime");

            migrationBuilder.RenameColumn(
                name: "From",
                schema: "PSN",
                table: "MsgMails",
                newName: "FromMailAddress");

            migrationBuilder.RenameColumn(
                name: "ExternalId",
                schema: "PSN",
                table: "MsgMails",
                newName: "CcRecipients");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ToRecipients",
                schema: "PSN",
                table: "MsgMails",
                newName: "To");

            migrationBuilder.RenameColumn(
                name: "SentTime",
                schema: "PSN",
                table: "MsgMails",
                newName: "SendTime");

            migrationBuilder.RenameColumn(
                name: "FromMailAddress",
                schema: "PSN",
                table: "MsgMails",
                newName: "From");

            migrationBuilder.RenameColumn(
                name: "CcRecipients",
                schema: "PSN",
                table: "MsgMails",
                newName: "ExternalId");

            migrationBuilder.AddColumn<string>(
                name: "Cc",
                schema: "PSN",
                table: "MsgMails",
                type: "text",
                nullable: true);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace PS.Notification.Infrastructure.Migrations
{
    public partial class Starter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "PSN");

            migrationBuilder.CreateTable(
                name: "MsgMails",
                schema: "PSN",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExternalId = table.Column<string>(type: "text", nullable: true),
                    ApplicationName = table.Column<string>(type: "text", nullable: true),
                    FromDisplayName = table.Column<string>(type: "text", nullable: true),
                    From = table.Column<string>(type: "text", nullable: true),
                    Subject = table.Column<string>(type: "text", nullable: true),
                    Body = table.Column<string>(type: "text", nullable: true),
                    To = table.Column<string>(type: "text", nullable: true),
                    Cc = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2021, 12, 27, 11, 13, 59, 381, DateTimeKind.Local).AddTicks(6417)),
                    IsSend = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    SendTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ErrorMessage = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MsgMails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MsgMailAttachments",
                schema: "PSN",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MailId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Content = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MsgMailAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MsgMailAttachments_MsgMails_MailId",
                        column: x => x.MailId,
                        principalSchema: "PSN",
                        principalTable: "MsgMails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MsgMailAttachments_MailId",
                schema: "PSN",
                table: "MsgMailAttachments",
                column: "MailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MsgMailAttachments",
                schema: "PSN");

            migrationBuilder.DropTable(
                name: "MsgMails",
                schema: "PSN");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace BeautyAndThePet.Data.Migrations
{
    public partial class AddSenderAndReceiverOfMessages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "To",
                table: "SentMessages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "From",
                table: "ReceivedMessages",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "To",
                table: "SentMessages");

            migrationBuilder.DropColumn(
                name: "From",
                table: "ReceivedMessages");
        }
    }
}

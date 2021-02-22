using Microsoft.EntityFrameworkCore.Migrations;

namespace BeautyAndThePet.Data.Migrations
{
    public partial class FromBecomesApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ads_AspNetUsers_FromId",
                table: "Ads");

            migrationBuilder.RenameColumn(
                name: "FromId",
                table: "Ads",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Ads_FromId",
                table: "Ads",
                newName: "IX_Ads_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ads_AspNetUsers_ApplicationUserId",
                table: "Ads",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ads_AspNetUsers_ApplicationUserId",
                table: "Ads");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Ads",
                newName: "FromId");

            migrationBuilder.RenameIndex(
                name: "IX_Ads_ApplicationUserId",
                table: "Ads",
                newName: "IX_Ads_FromId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ads_AspNetUsers_FromId",
                table: "Ads",
                column: "FromId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

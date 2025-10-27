using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantHotelAds.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixUserGuidKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_AspNetUsers_UserId1",
                table: "Hotels");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_AspNetUsers_UserId1",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_UserId1",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_UserId1",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Hotels");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "Restaurants",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "Hotels",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_UserId1",
                table: "Restaurants",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_UserId1",
                table: "Hotels",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_AspNetUsers_UserId1",
                table: "Hotels",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_AspNetUsers_UserId1",
                table: "Restaurants",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

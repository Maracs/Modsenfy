using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modsenfy.DataAccessLayer.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Stream",
                table: "Stream");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stream",
                table: "Stream",
                columns: new[] { "UserId", "TrackId", "StreamDate" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Stream",
                table: "Stream");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stream",
                table: "Stream",
                columns: new[] { "UserId", "TrackId" });
        }
    }
}

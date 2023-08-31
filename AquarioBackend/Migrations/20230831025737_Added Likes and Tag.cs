using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AquarioBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddedLikesandTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Threads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ThreadLikes",
                table: "Threads",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RepliesLikes",
                table: "Replies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Threads");

            migrationBuilder.DropColumn(
                name: "ThreadLikes",
                table: "Threads");

            migrationBuilder.DropColumn(
                name: "RepliesLikes",
                table: "Replies");
        }
    }
}

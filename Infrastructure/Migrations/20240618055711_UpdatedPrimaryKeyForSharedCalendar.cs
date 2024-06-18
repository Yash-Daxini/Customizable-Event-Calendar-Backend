using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedPrimaryKeyForSharedCalendar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SharedCalendar",
                table: "SharedCalendar");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "SharedCalendar",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SharedCalendar",
                table: "SharedCalendar",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SharedCalendar_SenderId",
                table: "SharedCalendar",
                column: "SenderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SharedCalendar",
                table: "SharedCalendar");

            migrationBuilder.DropIndex(
                name: "IX_SharedCalendar_SenderId",
                table: "SharedCalendar");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "SharedCalendar",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SharedCalendar",
                table: "SharedCalendar",
                columns: new[] { "SenderId", "ReceiverId" });
        }
    }
}

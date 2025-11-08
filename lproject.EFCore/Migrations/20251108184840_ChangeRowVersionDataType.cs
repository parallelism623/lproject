using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lproject.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRowVersionDataType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RowVersion",
                table: "EntityGenerateValues",
                type: "nvarchar(max)",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldRowVersion: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RowVersion",
                table: "EntityGenerateValues",
                type: "nvarchar(max)",
                rowVersion: true,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldRowVersion: true,
                oldNullable: true);
        }
    }
}

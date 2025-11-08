using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lproject.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAllColumnIntoAllNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ValueGeneratedOnUpdateSometimes",
                table: "EntityGenerateValues",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ValueGeneratedOnUpdate",
                table: "EntityGenerateValues",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ValueGeneratedOnAddOrUpdate",
                table: "EntityGenerateValues",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ValueGeneratedOnAdd",
                table: "EntityGenerateValues",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ValueGeneratedNever",
                table: "EntityGenerateValues",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DefaultValue",
                table: "EntityGenerateValues",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "DefaultValue",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "DefaultValue");

            migrationBuilder.AlterColumn<string>(
                name: "Concurrency",
                table: "EntityGenerateValues",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ValueGeneratedOnUpdateSometimes",
                table: "EntityGenerateValues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ValueGeneratedOnUpdate",
                table: "EntityGenerateValues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ValueGeneratedOnAddOrUpdate",
                table: "EntityGenerateValues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ValueGeneratedOnAdd",
                table: "EntityGenerateValues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ValueGeneratedNever",
                table: "EntityGenerateValues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DefaultValue",
                table: "EntityGenerateValues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "DefaultValue",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "DefaultValue");

            migrationBuilder.AlterColumn<string>(
                name: "Concurrency",
                table: "EntityGenerateValues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}

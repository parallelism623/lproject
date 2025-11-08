using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lproject.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EntityGenerateValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Computed = table.Column<string>(type: "nvarchar(max)", nullable: false, computedColumnSql: "'Ignore'"),
                    Identity = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Concurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefaultValue = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "DefaultValue"),
                    RowVersion = table.Column<string>(type: "nvarchar(max)", rowVersion: true, nullable: false),
                    ValueGeneratedNever = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValueGeneratedOnAdd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValueGeneratedOnAddOrUpdate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValueGeneratedOnUpdate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValueGeneratedOnUpdateSometimes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityGenerateValues", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntityGenerateValues");
        }
    }
}

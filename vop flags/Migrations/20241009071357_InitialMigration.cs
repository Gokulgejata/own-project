using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vop_flags.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flagdesign",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FlagName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Types = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Flagview = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flagdesign", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flagdesign");
        }
    }
}

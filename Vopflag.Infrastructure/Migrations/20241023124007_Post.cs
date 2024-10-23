using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vopflag.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Post : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FlagdesignId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FlagMaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlagMaterialType = table.Column<int>(type: "int", nullable: false),
                    BundleAvailability = table.Column<int>(type: "int", nullable: false),
                    SinglePiecePrice = table.Column<int>(type: "int", nullable: false),
                    SingleBundlePrice = table.Column<double>(type: "float", nullable: false),
                    FlagImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Post_FlagMaterial_FlagMaterialId",
                        column: x => x.FlagMaterialId,
                        principalTable: "FlagMaterial",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Post_Flagdesign_FlagdesignId",
                        column: x => x.FlagdesignId,
                        principalTable: "Flagdesign",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Post_FlagdesignId",
                table: "Post",
                column: "FlagdesignId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_FlagMaterialId",
                table: "Post",
                column: "FlagMaterialId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Post");
        }
    }
}

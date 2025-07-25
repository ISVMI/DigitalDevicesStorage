using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalDevices.CharacteristicsService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CharacteristicsType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DataType = table.Column<string>(type: "text", nullable: false),
                    EnumType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacteristicsType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CharacteristicsTypeProductTypes",
                columns: table => new
                {
                    CharacteristicsTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductTypesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacteristicsTypeProductTypes", x => new { x.ProductTypesId, x.CharacteristicsTypeId });
                });

            migrationBuilder.CreateTable(
                name: "Characteristics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    CharacteristicsTypeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characteristics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characteristics_CharacteristicsType_CharacteristicsTypeId",
                        column: x => x.CharacteristicsTypeId,
                        principalTable: "CharacteristicsType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characteristics_CharacteristicsTypeId",
                table: "Characteristics",
                column: "CharacteristicsTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Characteristics");

            migrationBuilder.DropTable(
                name: "CharacteristicsTypeProductTypes");

            migrationBuilder.DropTable(
                name: "CharacteristicsType");
        }
    }
}

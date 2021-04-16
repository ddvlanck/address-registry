using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AddressRegistry.Projections.Legacy.Migrations
{
    public partial class AddLinkedDataEventStream : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "AddressRegistryLdes");

            migrationBuilder.CreateTable(
                name: "Address",
                schema: "AddressRegistryLdes",
                columns: table => new
                {
                    Position = table.Column<long>(type: "bigint", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersistentLocalId = table.Column<int>(type: "int", nullable: true),
                    ChangeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetNameId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HouseNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PointPosition = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PositionMethod = table.Column<int>(type: "int", nullable: true),
                    PositionSpecification = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    IsOfficiallyAssigned = table.Column<bool>(type: "bit", nullable: false),
                    EventGeneratedAtTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ObjectIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecordCanBePublished = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Position)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateIndex(
                name: "CI_Address_Position",
                schema: "AddressRegistryLdes",
                table: "Address",
                column: "Position");

            migrationBuilder.CreateIndex(
                name: "IX_Address_AddressId",
                schema: "AddressRegistryLdes",
                table: "Address",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_PersistentLocalId",
                schema: "AddressRegistryLdes",
                table: "Address",
                column: "PersistentLocalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address",
                schema: "AddressRegistryLdes");
        }
    }
}

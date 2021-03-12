using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AddressRegistry.Projections.Legacy.Migrations
{
    public partial class AddTableForLinkedDataEventStream : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddressLinkedDataEventStream",
                schema: "AddressRegistryLegacy",
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
                    table.PrimaryKey("PK_AddressLinkedDataEventStream", x => x.Position)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateIndex(
                name: "CI_AddressLinkedDataEventStream_Position",
                schema: "AddressRegistryLegacy",
                table: "AddressLinkedDataEventStream",
                column: "Position");

            migrationBuilder.CreateIndex(
                name: "IX_AddressLinkedDataEventStream_AddressId",
                schema: "AddressRegistryLegacy",
                table: "AddressLinkedDataEventStream",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressLinkedDataEventStream_PersistentLocalId",
                schema: "AddressRegistryLegacy",
                table: "AddressLinkedDataEventStream",
                column: "PersistentLocalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddressLinkedDataEventStream",
                schema: "AddressRegistryLegacy");
        }
    }
}

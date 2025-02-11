using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxiService.Modules.Rides.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ride");

            migrationBuilder.CreateTable(
                name: "position",
                schema: "ride",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ride_id = table.Column<Guid>(type: "uuid", nullable: false),
                    from_latitude = table.Column<double>(type: "double precision", nullable: false),
                    from_longitude = table.Column<double>(type: "double precision", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    version = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_position", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ride",
                schema: "ride",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    passanger_id = table.Column<Guid>(type: "uuid", nullable: false),
                    driver_id = table.Column<Guid>(type: "uuid", nullable: true),
                    fare = table.Column<decimal>(type: "numeric", nullable: true),
                    distance = table.Column<double>(type: "double precision", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    from_latitude = table.Column<double>(type: "double precision", nullable: false),
                    from_longitude = table.Column<double>(type: "double precision", nullable: false),
                    to_latitude = table.Column<double>(type: "double precision", nullable: false),
                    to_longitude = table.Column<double>(type: "double precision", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    version = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ride", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_position_ride_id",
                schema: "ride",
                table: "position",
                column: "ride_id");

            migrationBuilder.CreateIndex(
                name: "ix_position_tenant_id",
                schema: "ride",
                table: "position",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "ix_ride_passanger_id_status",
                schema: "ride",
                table: "ride",
                columns: new[] { "passanger_id", "status" });

            migrationBuilder.CreateIndex(
                name: "ix_ride_tenant_id",
                schema: "ride",
                table: "ride",
                column: "tenant_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "position",
                schema: "ride");

            migrationBuilder.DropTable(
                name: "ride",
                schema: "ride");
        }
    }
}

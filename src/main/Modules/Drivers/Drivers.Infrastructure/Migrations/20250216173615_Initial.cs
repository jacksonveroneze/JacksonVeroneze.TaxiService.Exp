using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxiService.Modules.Drivers.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "driver");

            migrationBuilder.CreateTable(
                name: "driver",
                schema: "driver",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    birthday = table.Column<DateOnly>(type: "date", nullable: false),
                    gender = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    actived_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    inactived_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    cpf = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    phone = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    version = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_driver", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "driver_availability",
                schema: "driver",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    driver_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    latitude = table.Column<double>(type: "double precision", nullable: false),
                    longitude = table.Column<double>(type: "double precision", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    version = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_driver_availability", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vehicle",
                schema: "driver",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    driver_id = table.Column<Guid>(type: "uuid", nullable: false),
                    model = table.Column<string>(type: "text", nullable: false),
                    brand = table.Column<string>(type: "text", nullable: false),
                    plate = table.Column<string>(type: "text", nullable: false),
                    color = table.Column<string>(type: "text", nullable: false),
                    year = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    version = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vehicle", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_driver_status",
                schema: "driver",
                table: "driver",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "ix_driver_tenant_id",
                schema: "driver",
                table: "driver",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "ix_driver_availability_driver_id",
                schema: "driver",
                table: "driver_availability",
                column: "driver_id");

            migrationBuilder.CreateIndex(
                name: "ix_driver_availability_tenant_id",
                schema: "driver",
                table: "driver_availability",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "ix_vehicle_driver_id",
                schema: "driver",
                table: "vehicle",
                column: "driver_id");

            migrationBuilder.CreateIndex(
                name: "ix_vehicle_tenant_id",
                schema: "driver",
                table: "vehicle",
                column: "tenant_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "driver",
                schema: "driver");

            migrationBuilder.DropTable(
                name: "driver_availability",
                schema: "driver");

            migrationBuilder.DropTable(
                name: "vehicle",
                schema: "driver");
        }
    }
}

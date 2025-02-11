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
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
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
                name: "email",
                schema: "driver",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    driver_id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    version = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_email", x => x.id);
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
                name: "ix_email_driver_id",
                schema: "driver",
                table: "email",
                column: "driver_id");

            migrationBuilder.CreateIndex(
                name: "ix_email_tenant_id",
                schema: "driver",
                table: "email",
                column: "tenant_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "driver",
                schema: "driver");

            migrationBuilder.DropTable(
                name: "email",
                schema: "driver");
        }
    }
}

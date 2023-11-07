using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "phone",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    version = table.Column<int>(type: "integer", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_phone", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    birthday = table.Column<DateOnly>(type: "date", nullable: false),
                    gender = table.Column<int>(type: "integer", nullable: false),
                    cpf = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    actived_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    inactived_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    version = table.Column<int>(type: "integer", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "email",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    version = table.Column<int>(type: "integer", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_email", x => x.id);
                    table.ForeignKey(
                        name: "fk_email_user_entity_user_temp_id",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ride",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    driver_id = table.Column<Guid>(type: "uuid", nullable: true),
                    fare = table.Column<decimal>(type: "numeric", nullable: false),
                    distance = table.Column<double>(type: "double precision", nullable: false),
                    from_latitude = table.Column<double>(type: "double precision", nullable: true),
                    from_longitude = table.Column<double>(type: "double precision", nullable: true),
                    to_latitude = table.Column<double>(type: "double precision", nullable: true),
                    to_longitude = table.Column<double>(type: "double precision", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    version = table.Column<int>(type: "integer", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ride", x => x.id);
                    table.ForeignKey(
                        name: "fk_ride_user_entity_driver_temp_id3",
                        column: x => x.driver_id,
                        principalSchema: "public",
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_ride_user_entity_user_temp_id2",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "position",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ride_id = table.Column<Guid>(type: "uuid", nullable: false),
                    from_latitude = table.Column<double>(type: "double precision", nullable: true),
                    from_longitude = table.Column<double>(type: "double precision", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    version = table.Column<int>(type: "integer", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_position", x => x.id);
                    table.ForeignKey(
                        name: "fk_position_ride_entity_ride_entity_temp_id1",
                        column: x => x.ride_id,
                        principalSchema: "public",
                        principalTable: "ride",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_email_user_id",
                schema: "public",
                table: "email",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_position_ride_id",
                schema: "public",
                table: "position",
                column: "ride_id");

            migrationBuilder.CreateIndex(
                name: "ix_ride_driver_id",
                schema: "public",
                table: "ride",
                column: "driver_id");

            migrationBuilder.CreateIndex(
                name: "ix_ride_user_id",
                schema: "public",
                table: "ride",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "email",
                schema: "public");

            migrationBuilder.DropTable(
                name: "phone",
                schema: "public");

            migrationBuilder.DropTable(
                name: "position",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ride",
                schema: "public");

            migrationBuilder.DropTable(
                name: "user",
                schema: "public");
        }
    }
}

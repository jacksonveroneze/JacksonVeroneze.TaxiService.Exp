using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    private static readonly string[] columns = new[] { "user_id", "status" };
    private static readonly string[] columnsArray = new[] { "ride_id", "status" };

    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "public");

        migrationBuilder.CreateTable(
            name: "email",
            schema: "public",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                user_id = table.Column<Guid>(type: "uuid", nullable: false),
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

        migrationBuilder.CreateTable(
            name: "position",
            schema: "public",
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
            schema: "public",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                user_id = table.Column<Guid>(type: "uuid", nullable: false),
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

        migrationBuilder.CreateTable(
            name: "transaction",
            schema: "public",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                ride_id = table.Column<Guid>(type: "uuid", nullable: false),
                date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                status = table.Column<int>(type: "integer", nullable: false),
                ammount_value = table.Column<decimal>(type: "numeric", nullable: false),
                created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                version = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                tenant_id = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_transaction", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "user",
            schema: "public",
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
                table.PrimaryKey("pk_user", x => x.id);
            });

        migrationBuilder.CreateIndex(
            name: "ix_email_tenant_id",
            schema: "public",
            table: "email",
            column: "tenant_id");

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
            name: "ix_position_tenant_id",
            schema: "public",
            table: "position",
            column: "tenant_id");

        migrationBuilder.CreateIndex(
            name: "ix_ride_tenant_id",
            schema: "public",
            table: "ride",
            column: "tenant_id");

        migrationBuilder.CreateIndex(
            name: "ix_ride_user_id_status",
            schema: "public",
            table: "ride",
            columns: columns);

        migrationBuilder.CreateIndex(
            name: "ix_transaction_ride_id_status",
            schema: "public",
            table: "transaction",
            columns: columnsArray);

        migrationBuilder.CreateIndex(
            name: "ix_transaction_tenant_id",
            schema: "public",
            table: "transaction",
            column: "tenant_id");

        migrationBuilder.CreateIndex(
            name: "ix_user_status",
            schema: "public",
            table: "user",
            column: "status");

        migrationBuilder.CreateIndex(
            name: "ix_user_tenant_id",
            schema: "public",
            table: "user",
            column: "tenant_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "email",
            schema: "public");

        migrationBuilder.DropTable(
            name: "position",
            schema: "public");

        migrationBuilder.DropTable(
            name: "ride",
            schema: "public");

        migrationBuilder.DropTable(
            name: "transaction",
            schema: "public");

        migrationBuilder.DropTable(
            name: "user",
            schema: "public");
    }
}

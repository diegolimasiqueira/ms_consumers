#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace MsConsumers.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "shc_consumer");

            migrationBuilder.CreateTable(
                name: "tb_country_codes",
                schema: "shc_consumer",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    country_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_country_codes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_currencies",
                schema: "shc_consumer",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_currencies", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_languages",
                schema: "shc_consumer",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_languages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_time_zones",
                schema: "shc_consumer",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_time_zones", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_consumers",
                schema: "shc_consumer",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    document_id = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    photo_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    phone_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    currency_id = table.Column<Guid>(type: "uuid", nullable: false),
                    phone_country_code_id = table.Column<Guid>(type: "uuid", nullable: false),
                    preferred_language_id = table.Column<Guid>(type: "uuid", nullable: false),
                    timezone_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_consumers", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_consumers_tb_country_codes_phone_country_code_id",
                        column: x => x.phone_country_code_id,
                        principalSchema: "shc_consumer",
                        principalTable: "tb_country_codes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_consumers_tb_currencies_currency_id",
                        column: x => x.currency_id,
                        principalSchema: "shc_consumer",
                        principalTable: "tb_currencies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_consumers_tb_languages_preferred_language_id",
                        column: x => x.preferred_language_id,
                        principalSchema: "shc_consumer",
                        principalTable: "tb_languages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_consumers_tb_time_zones_timezone_id",
                        column: x => x.timezone_id,
                        principalSchema: "shc_consumer",
                        principalTable: "tb_time_zones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_consumer_address",
                schema: "shc_consumer",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    consumer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    street_address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    city = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    state = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    postalcode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    latitude = table.Column<double>(type: "double precision", nullable: true),
                    longitude = table.Column<double>(type: "double precision", nullable: true),
                    is_default = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    country_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_consumer_address", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_consumer_address_tb_consumers_consumer_id",
                        column: x => x.consumer_id,
                        principalSchema: "shc_consumer",
                        principalTable: "tb_consumers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_consumer_address_tb_country_codes_country_id",
                        column: x => x.country_id,
                        principalSchema: "shc_consumer",
                        principalTable: "tb_country_codes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_consumer_address_consumer_id",
                schema: "shc_consumer",
                table: "tb_consumer_address",
                column: "consumer_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_consumer_address_country_id",
                schema: "shc_consumer",
                table: "tb_consumer_address",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_consumers_currency_id",
                schema: "shc_consumer",
                table: "tb_consumers",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_consumers_document_id",
                schema: "shc_consumer",
                table: "tb_consumers",
                column: "document_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_consumers_email",
                schema: "shc_consumer",
                table: "tb_consumers",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_consumers_phone_country_code_id",
                schema: "shc_consumer",
                table: "tb_consumers",
                column: "phone_country_code_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_consumers_phone_number",
                schema: "shc_consumer",
                table: "tb_consumers",
                column: "phone_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_consumers_preferred_language_id",
                schema: "shc_consumer",
                table: "tb_consumers",
                column: "preferred_language_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_consumers_timezone_id",
                schema: "shc_consumer",
                table: "tb_consumers",
                column: "timezone_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_country_codes_code",
                schema: "shc_consumer",
                table: "tb_country_codes",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_currencies_code",
                schema: "shc_consumer",
                table: "tb_currencies",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_languages_code",
                schema: "shc_consumer",
                table: "tb_languages",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_time_zones_name",
                schema: "shc_consumer",
                table: "tb_time_zones",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_consumer_address",
                schema: "shc_consumer");

            migrationBuilder.DropTable(
                name: "tb_consumers",
                schema: "shc_consumer");

            migrationBuilder.DropTable(
                name: "tb_country_codes",
                schema: "shc_consumer");

            migrationBuilder.DropTable(
                name: "tb_currencies",
                schema: "shc_consumer");

            migrationBuilder.DropTable(
                name: "tb_languages",
                schema: "shc_consumer");

            migrationBuilder.DropTable(
                name: "tb_time_zones",
                schema: "shc_consumer");
        }
    }
}

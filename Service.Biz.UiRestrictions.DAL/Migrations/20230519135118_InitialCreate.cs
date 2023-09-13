using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Service.Biz.UiRestrictions.DAL.Entities;

#nullable disable

namespace Service.Biz.UiRestrictions.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:access_restriction_type", "tariff_not_enough")
                .Annotation("Npgsql:Enum:access_type", "disable,hidden");

            migrationBuilder.CreateTable(
                name: "feature",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_feature", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "organization_product",
                columns: table => new
                {
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    organization_id = table.Column<Guid>(type: "uuid", nullable: false),
                    network_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization_product", x => new { x.organization_id, x.product_id });
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    level = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product_feature_access",
                columns: table => new
                {
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    feature_id = table.Column<int>(type: "integer", nullable: false),
                    access_type = table.Column<AccessType>(type: "access_type", nullable: false),
                    access_restriction_type = table.Column<AccessRestrictionType>(type: "access_restriction_type", nullable: false),
                    detail = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_feature_access", x => new { x.product_id, x.feature_id });
                    table.ForeignKey(
                        name: "FK_product_feature_access_feature_feature_id",
                        column: x => x.feature_id,
                        principalTable: "feature",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_feature_access_product_product_id",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_feature_name",
                table: "feature",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_organization_product_network_id",
                table: "organization_product",
                column: "network_id");

            migrationBuilder.CreateIndex(
                name: "IX_organization_product_organization_id",
                table: "organization_product",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_feature_access_feature_id",
                table: "product_feature_access",
                column: "feature_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "organization_product");

            migrationBuilder.DropTable(
                name: "product_feature_access");

            migrationBuilder.DropTable(
                name: "feature");

            migrationBuilder.DropTable(
                name: "product");
        }
    }
}

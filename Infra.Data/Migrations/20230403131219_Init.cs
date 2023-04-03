using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "listings",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    listing_url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    property_type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_listings", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "calendars",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    listing_id = table.Column<long>(type: "bigint", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    available = table.Column<bool>(type: "bit", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_calendars", x => x.id);
                    table.ForeignKey(
                        name: "fk_calendars_listings_listing_id",
                        column: x => x.listing_id,
                        principalTable: "listings",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    listing_id = table.Column<long>(type: "bigint", nullable: false),
                    reviewer_id = table.Column<long>(type: "bigint", nullable: false),
                    reviewer_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reviews", x => x.id);
                    table.ForeignKey(
                        name: "fk_reviews_listings_listing_id",
                        column: x => x.listing_id,
                        principalTable: "listings",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_calendars_listing_id",
                table: "calendars",
                column: "listing_id");

            migrationBuilder.CreateIndex(
                name: "ix_reviews_listing_id",
                table: "reviews",
                column: "listing_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "calendars");

            migrationBuilder.DropTable(
                name: "reviews");

            migrationBuilder.DropTable(
                name: "listings");
        }
    }
}

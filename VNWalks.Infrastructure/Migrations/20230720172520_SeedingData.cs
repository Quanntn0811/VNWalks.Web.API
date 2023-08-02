using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VNWalks.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("5223651c-9784-4ad7-a596-bd8cf69a5725"), "Medium" },
                    { new Guid("5daca6f3-3e6d-40fa-9c54-e51e2fc60b62"), "Easy" },
                    { new Guid("747cf14c-a757-4f16-9170-2a2069e78541"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("2459c408-89c1-4947-86d9-d2cebff3352b"), "42", "Lam Dong", null },
                    { new Guid("42a416b2-36df-4f89-82d7-d77873208da7"), "53", "Tien Giang", null },
                    { new Guid("72bae868-bc5c-48f8-a8ab-e29ca99e5dfc"), "29", "Nghe An", null },
                    { new Guid("d43c4ba3-c173-4e35-8f59-a98c3adc496c"), "30", "Ha Tinh", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("5223651c-9784-4ad7-a596-bd8cf69a5725"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("5daca6f3-3e6d-40fa-9c54-e51e2fc60b62"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("747cf14c-a757-4f16-9170-2a2069e78541"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("2459c408-89c1-4947-86d9-d2cebff3352b"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("42a416b2-36df-4f89-82d7-d77873208da7"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("72bae868-bc5c-48f8-a8ab-e29ca99e5dfc"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("d43c4ba3-c173-4e35-8f59-a98c3adc496c"));
        }
    }
}

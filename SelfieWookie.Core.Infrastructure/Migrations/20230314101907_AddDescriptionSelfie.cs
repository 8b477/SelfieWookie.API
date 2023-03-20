using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SelfieWookie.Core.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionSelfie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Titleescription",
                table: "Selfie",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Selfie",
                newName: "Titleescription");
        }
    }
}

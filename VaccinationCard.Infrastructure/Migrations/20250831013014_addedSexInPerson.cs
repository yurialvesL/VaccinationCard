using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VaccinationCard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedSexInPerson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Sex",
                table: "Persons",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sex",
                table: "Persons");
        }
    }
}

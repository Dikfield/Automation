using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDestinies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TravelDate",
                table: "Clients");

            migrationBuilder.CreateTable(
                name: "Destinies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Country = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    TravelDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Persons = table.Column<int>(type: "INTEGER", nullable: false),
                    Tours = table.Column<int>(type: "INTEGER", nullable: false),
                    ClientId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Destinies_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Destinies_ClientId",
                table: "Destinies",
                column: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Destinies");

            migrationBuilder.AddColumn<DateTime>(
                name: "TravelDate",
                table: "Clients",
                type: "TEXT",
                nullable: true);
        }
    }
}

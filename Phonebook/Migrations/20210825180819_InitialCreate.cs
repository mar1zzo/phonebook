using Microsoft.EntityFrameworkCore.Migrations;

namespace Phonebook.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhoneNumberType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumberType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonPhone",
                columns: table => new
                {
                    BusinnesEntityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PhoneNumberTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonPhone", x => x.BusinnesEntityID);
                    table.ForeignKey(
                        name: "FK_PersonPhone_PhoneNumberType_PhoneNumberTypeID",
                        column: x => x.PhoneNumberTypeID,
                        principalTable: "PhoneNumberType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonPhone_PhoneNumberTypeID",
                table: "PersonPhone",
                column: "PhoneNumberTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonPhone");

            migrationBuilder.DropTable(
                name: "PhoneNumberType");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace SyncServer.Migrations
{
    public partial class DynamicEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DynamicEntities",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RowVersion = table.Column<int>(nullable: false),
                    Data = table.Column<string>(nullable: true),
                    ProjectTableName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DynamicEntities_ProjectTable_ProjectTableName",
                        column: x => x.ProjectTableName,
                        principalTable: "ProjectTable",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DynamicEntities_ProjectTableName",
                table: "DynamicEntities",
                column: "ProjectTableName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DynamicEntities");
        }
    }
}

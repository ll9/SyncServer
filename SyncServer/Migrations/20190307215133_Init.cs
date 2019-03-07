using Microsoft.EntityFrameworkCore.Migrations;

namespace SyncServer.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTable",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    ProjectId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTable", x => x.Name);
                    table.ForeignKey(
                        name: "FK_ProjectTable_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SchemaDefinitions",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Columns = table.Column<string>(nullable: true),
                    ProjectTableName = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RowVersion = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchemaDefinitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchemaDefinitions_ProjectTable_ProjectTableName",
                        column: x => x.ProjectTableName,
                        principalTable: "ProjectTable",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTable_ProjectId",
                table: "ProjectTable",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SchemaDefinitions_ProjectTableName",
                table: "SchemaDefinitions",
                column: "ProjectTableName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchemaDefinitions");

            migrationBuilder.DropTable(
                name: "ProjectTable");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}

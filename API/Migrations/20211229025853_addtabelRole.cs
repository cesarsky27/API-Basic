using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addtabelRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_m_role",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_role", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_accountrole",
                columns: table => new
                {
                    AccountRoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MyProperty = table.Column<int>(type: "int", nullable: false),
                    AccountNIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_accountrole", x => x.AccountRoleID);
                    table.ForeignKey(
                        name: "FK_tb_m_accountrole_tb_m_account_AccountNIK",
                        column: x => x.AccountNIK,
                        principalTable: "tb_m_account",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_m_accountrole_tb_m_role_RoleID",
                        column: x => x.RoleID,
                        principalTable: "tb_m_role",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_accountrole_AccountNIK",
                table: "tb_m_accountrole",
                column: "AccountNIK");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_accountrole_RoleID",
                table: "tb_m_accountrole",
                column: "RoleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_m_accountrole");

            migrationBuilder.DropTable(
                name: "tb_m_role");
        }
    }
}

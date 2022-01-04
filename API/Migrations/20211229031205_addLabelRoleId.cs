using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addLabelRoleId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_accountrole_tb_m_role_RoleID",
                table: "tb_m_accountrole");

            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "tb_m_accountrole");

            migrationBuilder.RenameColumn(
                name: "RoleID",
                table: "tb_m_accountrole",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_tb_m_accountrole_RoleID",
                table: "tb_m_accountrole",
                newName: "IX_tb_m_accountrole_RoleId");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "tb_m_accountrole",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_accountrole_tb_m_role_RoleId",
                table: "tb_m_accountrole",
                column: "RoleId",
                principalTable: "tb_m_role",
                principalColumn: "RoleID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_accountrole_tb_m_role_RoleId",
                table: "tb_m_accountrole");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "tb_m_accountrole",
                newName: "RoleID");

            migrationBuilder.RenameIndex(
                name: "IX_tb_m_accountrole_RoleId",
                table: "tb_m_accountrole",
                newName: "IX_tb_m_accountrole_RoleID");

            migrationBuilder.AlterColumn<int>(
                name: "RoleID",
                table: "tb_m_accountrole",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "tb_m_accountrole",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_accountrole_tb_m_role_RoleID",
                table: "tb_m_accountrole",
                column: "RoleID",
                principalTable: "tb_m_role",
                principalColumn: "RoleID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

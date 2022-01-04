using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addmodelemployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_table_m_employee",
                table: "table_m_employee");

            migrationBuilder.RenameTable(
                name: "table_m_employee",
                newName: "tb_m_employee");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_m_employee",
                table: "tb_m_employee",
                column: "NIK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_m_employee",
                table: "tb_m_employee");

            migrationBuilder.RenameTable(
                name: "tb_m_employee",
                newName: "table_m_employee");

            migrationBuilder.AddPrimaryKey(
                name: "PK_table_m_employee",
                table: "table_m_employee",
                column: "NIK");
        }
    }
}

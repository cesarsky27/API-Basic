using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addmodelprofiling2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_tb_m_employee_NIK",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Profillings_Accounts_NIK",
                table: "Profillings");

            migrationBuilder.DropForeignKey(
                name: "FK_Profillings_tb_m_education_EducationID",
                table: "Profillings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Profillings",
                table: "Profillings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.RenameTable(
                name: "Profillings",
                newName: "tb_t_profilling");

            migrationBuilder.RenameTable(
                name: "Accounts",
                newName: "tb_m_account");

            migrationBuilder.RenameIndex(
                name: "IX_Profillings_EducationID",
                table: "tb_t_profilling",
                newName: "IX_tb_t_profilling_EducationID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_t_profilling",
                table: "tb_t_profilling",
                column: "NIK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_m_account",
                table: "tb_m_account",
                column: "NIK");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_account_tb_m_employee_NIK",
                table: "tb_m_account",
                column: "NIK",
                principalTable: "tb_m_employee",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_t_profilling_tb_m_account_NIK",
                table: "tb_t_profilling",
                column: "NIK",
                principalTable: "tb_m_account",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_t_profilling_tb_m_education_EducationID",
                table: "tb_t_profilling",
                column: "EducationID",
                principalTable: "tb_m_education",
                principalColumn: "EducationID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_account_tb_m_employee_NIK",
                table: "tb_m_account");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_t_profilling_tb_m_account_NIK",
                table: "tb_t_profilling");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_t_profilling_tb_m_education_EducationID",
                table: "tb_t_profilling");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_t_profilling",
                table: "tb_t_profilling");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_m_account",
                table: "tb_m_account");

            migrationBuilder.RenameTable(
                name: "tb_t_profilling",
                newName: "Profillings");

            migrationBuilder.RenameTable(
                name: "tb_m_account",
                newName: "Accounts");

            migrationBuilder.RenameIndex(
                name: "IX_tb_t_profilling_EducationID",
                table: "Profillings",
                newName: "IX_Profillings_EducationID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Profillings",
                table: "Profillings",
                column: "NIK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "NIK");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_tb_m_employee_NIK",
                table: "Accounts",
                column: "NIK",
                principalTable: "tb_m_employee",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Profillings_Accounts_NIK",
                table: "Profillings",
                column: "NIK",
                principalTable: "Accounts",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Profillings_tb_m_education_EducationID",
                table: "Profillings",
                column: "EducationID",
                principalTable: "tb_m_education",
                principalColumn: "EducationID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

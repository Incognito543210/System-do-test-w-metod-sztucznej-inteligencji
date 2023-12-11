using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class add_coulm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ParamaInfo",
                table: "ParamaInfo");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ParamaInfo");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ParamaInfo",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ParamaInfo",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "LowerBoundary",
                table: "ParamaInfo",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Step",
                table: "ParamaInfo",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "UpperBoundary",
                table: "ParamaInfo",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParamaInfo",
                table: "ParamaInfo",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ParamaInfo",
                table: "ParamaInfo");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ParamaInfo");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ParamaInfo");

            migrationBuilder.DropColumn(
                name: "LowerBoundary",
                table: "ParamaInfo");

            migrationBuilder.DropColumn(
                name: "Step",
                table: "ParamaInfo");

            migrationBuilder.DropColumn(
                name: "UpperBoundary",
                table: "ParamaInfo");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ParamaInfo",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParamaInfo",
                table: "ParamaInfo",
                column: "Id");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiMedicalOffice.Migrations
{
    /// <inheritdoc />
    public partial class AddCamposDataCriacaoEAtualizacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Pacientes",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Pacientes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Pacientes");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DesafioBackend.Migrations
{
    /// <inheritdoc />
    public partial class AtualizaTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "entregador",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    identificador = table.Column<string>(type: "text", nullable: false),
                    nome = table.Column<string>(type: "text", nullable: false),
                    cnpj = table.Column<string>(type: "text", nullable: false),
                    data_nascimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    numero_cnh = table.Column<string>(type: "text", nullable: false),
                    tipo_cnh = table.Column<string>(type: "text", nullable: false),
                    imagem_cnh = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entregador", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "locacao",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    identificador = table.Column<string>(type: "text", nullable: false),
                    valor_diaria = table.Column<decimal>(type: "numeric", nullable: false),
                    entregador_id = table.Column<long>(type: "bigint", nullable: false),
                    moto_id = table.Column<long>(type: "bigint", nullable: false),
                    data_inicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_termino = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_previsao_termino = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_devolucao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    plano = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_locacao", x => x.id);
                    table.ForeignKey(
                        name: "FK_locacao_entregador_entregador_id",
                        column: x => x.entregador_id,
                        principalTable: "entregador",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_locacao_moto_moto_id",
                        column: x => x.moto_id,
                        principalTable: "moto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_locacao_entregador_id",
                table: "locacao",
                column: "entregador_id");

            migrationBuilder.CreateIndex(
                name: "IX_locacao_moto_id",
                table: "locacao",
                column: "moto_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "locacao");

            migrationBuilder.DropTable(
                name: "entregador");
        }
    }
}

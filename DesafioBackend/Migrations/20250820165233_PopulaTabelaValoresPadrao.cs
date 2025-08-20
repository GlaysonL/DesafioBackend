using Microsoft.EntityFrameworkCore.Migrations;
using System.Collections.Generic;
using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;

#nullable disable

namespace DesafioBackend.Migrations
{
    /// <inheritdoc />
    public partial class PopulaTabelaValoresPadrao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Inserindo moto
            migrationBuilder.Sql(@"
        INSERT INTO ""moto"" (identificador, ano, modelo, placa)
        VALUES ('moto123', 2020, 'Mottu Sport', 'CDX-0101');
        INSERT INTO ""moto"" (identificador, ano, modelo, placa)
        VALUES ('moto456', 2021, 'Mottu City', 'EFG-0202');
        INSERT INTO ""moto"" (identificador, ano, modelo, placa)
        VALUES ('moto789', 2022, 'Mottu Urban', 'HIJ-0303');
    ");

            // Inserindo entregador
            migrationBuilder.Sql(@"
        INSERT INTO ""entregador"" (identificador, nome, cnpj, data_nascimento, numero_cnh, tipo_cnh, imagem_cnh)
        VALUES ('entregador123', 'João da Silva', '12345678901234', '1990-01-01', '12345678900', 'A', 'base64string');
        INSERT INTO ""entregador"" (identificador, nome, cnpj, data_nascimento, numero_cnh, tipo_cnh, imagem_cnh)
        VALUES ('entregador456', 'Maria Souza', '23456789012345', '1985-05-10', '23456789001', 'B', 'base64string2');
        INSERT INTO ""entregador"" (identificador, nome, cnpj, data_nascimento, numero_cnh, tipo_cnh, imagem_cnh)
        VALUES ('entregador789', 'Carlos Lima', '34567890123456', '1992-09-20', '34567890123', 'A', 'base64string3');
    ");

            // Inserindo locação
            migrationBuilder.Sql(@"
        INSERT INTO ""locacao"" (identificador, valor_diaria, entregador_id, moto_id, data_inicio, data_termino, data_previsao_termino, data_devolucao, plano)
        VALUES ('locacao123', 10.00, 1, 1, '2024-01-01', '2024-01-07', '2024-01-07', NULL, 7);
        INSERT INTO ""locacao"" (identificador, valor_diaria, entregador_id, moto_id, data_inicio, data_termino, data_previsao_termino, data_devolucao, plano)
        VALUES ('locacao456', 12.00, 2, 2, '2024-02-01', '2024-02-15', '2024-02-15', NULL, 15);
        INSERT INTO ""locacao"" (identificador, valor_diaria, entregador_id, moto_id, data_inicio, data_termino, data_previsao_termino, data_devolucao, plano)
        VALUES ('locacao789', 15.00, 3, 3, '2024-03-01', '2024-03-30', '2024-03-30', NULL, 30);
    ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

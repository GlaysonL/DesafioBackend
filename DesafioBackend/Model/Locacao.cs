using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioBackend.Model;

[Table("locacao")]
public class Locacao
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [Column("identificador")]
    public string Identificador { get; set; }

    [Column("valor_diaria")]
    public decimal ValorDiaria { get; set; }

    [Required]
    [Column("entregador_id")]
    public long EntregadorId { get; set; }
    [ForeignKey("EntregadorId")]
    public Entregador Entregador { get; set; }

    [Required]
    [Column("moto_id")]
    public long MotoId { get; set; }
    [ForeignKey("MotoId")]
    public Moto Moto { get; set; }

    [Required]
    [Column("data_inicio")]
    public DateTime DataInicio { get; set; }

    [Required]
    [Column("data_termino")]
    public DateTime DataTermino { get; set; }

    [Required]
    [Column("data_previsao_termino")]
    public DateTime DataPrevisaoTermino { get; set; }

    [Column("data_devolucao")]
    public DateTime? DataDevolucao { get; set; }

    [Required]
    [Column("plano")]
    public int Plano { get; set; }
}

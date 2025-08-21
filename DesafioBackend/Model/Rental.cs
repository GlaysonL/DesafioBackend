using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DesafioBackend.Model;

[Table("locacao")]
public class Rental
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    [JsonIgnore]
    public long Id { get; set; }

    [Required]
    [Column("identificador")]
    [JsonPropertyName("identificador")]
    public string Identifier { get; set; }

    [Column("valor_diaria")]
    [JsonPropertyName("valor_diaria")]
    public decimal DailyRate { get; set; }

    [Required]
    [Column("entregador_id")]
    [JsonPropertyName("entregador_id")]
    public long DeliveryDriverId { get; set; }
    [ForeignKey("DeliveryDriverId")]
    [JsonPropertyName("entregador")]
    public DeliveryDriver DeliveryDriver { get; set; }

    [Required]
    [Column("moto_id")]
    [JsonPropertyName("moto_id")]
    public long MotorcycleId { get; set; }
    [ForeignKey("MotorcycleId")]
    [JsonPropertyName("moto")]
    public Motorcycle Motorcycle { get; set; }

    [Required]
    [Column("data_inicio")]
    [JsonPropertyName("data_inicio")]
    public DateTime StartDate { get; set; }

    [Required]
    [Column("data_termino")]
    [JsonPropertyName("data_termino")]
    public DateTime EndDate { get; set; }

    [Required]
    [Column("data_previsao_termino")]
    [JsonPropertyName("data_previsao_termino")]
    public DateTime ExpectedEndDate { get; set; }

    [Column("data_devolucao")]
    [JsonPropertyName("data_devolucao")]
    public DateTime? ReturnDate { get; set; }

    [Required]
    [Column("plano")]
    [JsonPropertyName("plano")]
    public int Plan { get; set; }
}

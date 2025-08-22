using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DesafioBackend.Model;

[Table("entregador")]
public class DeliveryDriver
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    [JsonIgnore]
    public long Id { get; set; }

    [Required]
    [Column("identificador")]
    [JsonPropertyName("identificador")]
    public required string Identifier { get; set; }

    [Required]
    [Column("nome")]
    [JsonPropertyName("nome")]
    public required string Name { get; set; }

    [Required]
    [Column("cnpj")]
    [JsonPropertyName("cnpj")]
    public required string Cnpj { get; set; }

    [Required]
    [Column("data_nascimento")]
    [JsonPropertyName("data_nascimento")]
    public required DateTime BirthDate { get; set; }

    [Required]
    [Column("numero_cnh")]
    [JsonPropertyName("numero_cnh")]
    public required string CnhNumber { get; set; }

    [Required]
    [Column("tipo_cnh")]
    [JsonPropertyName("tipo_cnh")]
    public required string CnhType { get; set; }

    [Required]
    [Column("imagem_cnh")]
    [JsonPropertyName("imagem_cnh")]
    public required string CnhImage { get; set; }
}

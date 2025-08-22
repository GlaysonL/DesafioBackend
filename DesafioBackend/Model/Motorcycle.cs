using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace DesafioBackend.Model;

[Table("moto")]
[Index(nameof(Plate), IsUnique = true)]
public class Motorcycle
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

    [Column("ano")]
    [JsonPropertyName("ano")]
    public int Year { get; set; }

    [Required]
    [Column("modelo")]
    [JsonPropertyName("modelo")]
    public required string Model { get; set; }

    [Required]
    [Column("placa")]
    [JsonPropertyName("placa")]
    public required string Plate { get; set; }
}

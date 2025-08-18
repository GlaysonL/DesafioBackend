using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioBackend.Model;

[Table("moto")]
public class Moto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public long Id { get; set; }
    [Column("identificador")]
    public string Identificador { get; set; }
    [Column("ano")]
    public int Ano { get; set; }
    [Column("modelo")] 
    public string Modelo { get; set; }
    [Column("placa")]
    public string Placa { get; set; }
}


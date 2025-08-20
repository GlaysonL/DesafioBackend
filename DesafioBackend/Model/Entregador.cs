using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioBackend.Model;

[Table("entregador")]
public class Entregador
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [Column("identificador")]
    public string Identificador { get; set; }

    [Required]
    [Column("nome")]
    public string Nome { get; set; }

    [Required]
    [Column("cnpj")]
    public string Cnpj { get; set; }

    [Required]
    [Column("data_nascimento")]
    public DateTime DataNascimento { get; set; }

    [Required]
    [Column("numero_cnh")]
    public string NumeroCnh { get; set; }

    [Required]
    [Column("tipo_cnh")]
    public string TipoCnh { get; set; }

    [Required]
    [Column("imagem_cnh")]
    public string ImagemCnh { get; set; }
}

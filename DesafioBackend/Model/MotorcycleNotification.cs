using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioBackend.Model
{
    [Table("moto_notification")]
    public class MotorcycleNotification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("motorcycleid")]
        public long MotorcycleId { get; set; }

        [Required]
        [Column("identificador")]
        public required string Identifier { get; set; }

        [Required]
        [Column("modelo")]
        public required string Model { get; set; }

        [Required]
        [Column("placa")]
        public required string Plate { get; set; }

        [Column("ano")]
        public required int Year { get; set; }

        [Column("receivedat")]
        public required DateTime ReceivedAt { get; set; }
    }
}

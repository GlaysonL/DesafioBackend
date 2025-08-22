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
        public string Identifier { get; set; }

        [Required]
        [Column("modelo")]
        public string Model { get; set; }

        [Required]
        [Column("placa")]
        public string Plate { get; set; }

        [Column("ano")]
        public int Year { get; set; }

        [Column("receivedat")]
        public DateTime ReceivedAt { get; set; }
    }
}

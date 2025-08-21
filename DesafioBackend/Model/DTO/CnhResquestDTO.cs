using System.Text.Json.Serialization;

namespace DesafioBackend.Model.DTO
{
    public class CnhResquestDTO
    {
        [JsonPropertyName("imagem_cnh")]
        public string ImagemCnh { get; set; }
    }
}

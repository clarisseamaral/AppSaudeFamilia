using ColetaApi.Data;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ColetaApi.Dtos
{
    [DataContract]
    public class TipoPerguntaDto
    {
        public TipoPerguntaDto(TipoPergunta pergunta)
        {
            Id = pergunta.Id;
            Descricao = pergunta.Descricao;
        }

        [Required]
        [DataMember]
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "Descrição")]
        [DataMember]
        public string Descricao { get; set; }
    }
}

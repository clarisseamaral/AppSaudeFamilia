using ColetaApi.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ColetaApi.Dtos
{
    [DataContract]
    public class PerguntaDto
    {
        public PerguntaDto() { }

        public PerguntaDto(Pergunta pergunta)
        {
            Id = pergunta.Id;
            Descricao = pergunta.Descricao;
            IdTipoPergunta = pergunta.IdTipoPergunta;
            TipoPergunta = pergunta.IdTipoPerguntaNavigation?.Descricao;
            Alternativas = pergunta.OpcaoRespostaPergunta?.Select(o => new AlternativaDto(o));
        }

        [Required]
        [DataMember]
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "Descrição")]
        [DataMember]
        public string Descricao { get; set; }

        [Required]
        [Display(Name = "Id do tipo da pergunta")]
        [DataMember]
        public int IdTipoPergunta { get; set; }

        [Required]
        [Display(Name = "Tipo da pergunta")]
        [DataMember]
        public string TipoPergunta { get; set; }

        [Display(Name = "Opções de resposta")]
        [DataMember]
        public IEnumerable<AlternativaDto> Alternativas { get; set; }
    }
}
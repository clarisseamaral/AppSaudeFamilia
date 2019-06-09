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
    public class QuestionarioDto
    {
        public QuestionarioDto() { }

        public QuestionarioDto(Questionario questionario)
        {
            Id = questionario.Id;
            Nome = questionario.Nome;
        }

        [Required]
        [DataMember]
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        [DataMember]
        public string Nome { get; set; }
    }
}
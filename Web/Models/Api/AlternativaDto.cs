using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Coleta.Models.Api
{
    [DataContract]
    public class AlternativaDto
    {
        public AlternativaDto() { }

        public AlternativaDto(OpcaoRespostaPergunta opcao)
        {
            Id = opcao.id;
            Texto = opcao.opcao;
        }

        [Required]
        [DataMember]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [DataMember]
        public string Texto { get; set; }
    }
}
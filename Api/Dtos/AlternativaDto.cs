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
    public class AlternativaDto
    {
        public AlternativaDto() { }

        public AlternativaDto(OpcaoRespostaPergunta opcao)
        {
            Id = opcao.Id;
            Texto = opcao.Opcao;
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
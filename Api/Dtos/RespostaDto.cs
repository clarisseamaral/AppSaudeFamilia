using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ColetaApi.Dtos
{
    [DataContract]
    public class RespostaDto
    {
        [DataMember]
        public int IdPergunta { get; set; }

        [DataMember]
        public int? IdOpcaoResposta { get; set; }

        [StringLength(500)]
        [DataMember]
        public string Valor { get; set; }
    }
}

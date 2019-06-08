using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AppSaudeFamilia.Servico
{
    [DataContract]
    public class ColetaEntradaDTO
    {
        [DataMember]
        public DateTime Data { get; set; }

        [DataMember]
        public double Latitude { get; set; }

        [DataMember]
        public double Longitude { get; set; }

        [DataMember]
        public IList<RespostaDto> Respostas { get; set; }
    }

    [DataContract]
    public class RespostaDto
    {
        [DataMember]
        public int IdPergunta { get; set; }

        [DataMember]
        public int? IdOpcaoResposta { get; set; }

        [DataMember]
        public string Valor { get; set; }
    }
}
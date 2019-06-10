using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ColetaApi.Dtos
{
    [DataContract]
    public class ColetaDto
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
}

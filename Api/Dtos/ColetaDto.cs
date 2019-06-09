using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

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

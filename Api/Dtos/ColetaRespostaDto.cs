using ColetaApi.Data;
using System;
using System.Runtime.Serialization;

namespace ColetaApi.Dtos
{
    [DataContract]
    public class ColetaRespostaDto
    {
        public ColetaRespostaDto(Coleta coleta)
        {
            IdColeta = coleta.Id;
            Data = coleta.Data;
            IdUsuario = coleta.IdUsuario;
        }

        [DataMember]
        public int IdColeta { get; set; }

        [DataMember]
        public string NomeUsuario { get; set; }

        [DataMember]
        public int IdUsuario { get; set; }

        [DataMember]
        public DateTime Data { get; set; }

    }
}

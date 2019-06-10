using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ColetaApi.Dtos
{
    [DataContract]
    public class QuestionarioPerguntaDto
    {
        [DataMember]
        public string Descricao { get; set; }

        [DataMember]
        public IList<PerguntaSelecionadaDto> Perguntas { get; set; }
    }

    [DataContract]
    public class PerguntaSelecionadaDto
    {
        [DataMember]
        public int IdPergunta { get; set; }

        [DataMember]
        public int Ordem { get; set; }
    }
}

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ColetaApi.Dtos
{
    [DataContract]
    public class QuestionarioPerguntaDto
    {
        public string Descricao { get; set; }

        public IList<PerguntaSelecionadaDto> Perguntas { get; set; }
    }

    [DataContract]
    public class PerguntaSelecionadaDto
    {
        public int IdPergunta { get; set; }

        public int Ordem { get; set; }
    }
}

using ColetaApi.Data;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ColetaApi.Dtos
{
    [DataContract]
    public class DetalhesRespostaDto
    {
        public DetalhesRespostaDto(RespostaColeta coleta)
        {
            IdColeta = coleta.IdColeta;
            IdOpcaoResposta = coleta.IdOpcaoResposta;
            IdPergunta = coleta.IdPergunta;
            Pergunta = new PerguntaDto(coleta.IdPerguntaNavigation);
            Alternativas = new AlternativaDto(coleta.IdOpcaoRespostaNavigation);
            Valor = coleta.Valor;
        }

        [DataMember]
        public int IdColeta { get; set; }

        [DataMember]
        public int IdPergunta { get; set; }

        [DataMember]
        public int? IdOpcaoResposta { get; set; }

        [DataMember]
        public string Valor { get; set; }

        [DataMember]
        public PerguntaDto Pergunta { get; set; }

        [DataMember]
        public AlternativaDto Alternativas { get; set; }
    }
}

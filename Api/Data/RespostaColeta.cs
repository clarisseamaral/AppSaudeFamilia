using System;
using System.Collections.Generic;

namespace ColetaApi.Data
{
    public partial class RespostaColeta
    {
        public int IdColeta { get; set; }
        public int IdPergunta { get; set; }
        public int? IdOpcaoResposta { get; set; }
        public string Valor { get; set; }

        public Coleta IdColetaNavigation { get; set; }
        public OpcaoRespostaPergunta IdOpcaoRespostaNavigation { get; set; }
        public Pergunta IdPerguntaNavigation { get; set; }
    }
}

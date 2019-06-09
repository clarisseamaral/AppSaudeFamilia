using System;
using System.Collections.Generic;

namespace ColetaApi.Data
{
    public partial class Pergunta
    {
        public Pergunta()
        {
            OpcaoRespostaPergunta = new HashSet<OpcaoRespostaPergunta>();
            QuestionarioPergunta = new HashSet<QuestionarioPergunta>();
            RespostaColeta = new HashSet<RespostaColeta>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public int IdTipoPergunta { get; set; }
        public int Ordem { get; set; }

        public TipoPergunta IdTipoPerguntaNavigation { get; set; }
        public ICollection<OpcaoRespostaPergunta> OpcaoRespostaPergunta { get; set; }
        public ICollection<QuestionarioPergunta> QuestionarioPergunta { get; set; }
        public ICollection<RespostaColeta> RespostaColeta { get; set; }
    }
}

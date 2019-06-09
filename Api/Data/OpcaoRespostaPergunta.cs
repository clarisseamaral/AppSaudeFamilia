using System;
using System.Collections.Generic;

namespace ColetaApi.Data
{
    public partial class OpcaoRespostaPergunta
    {
        public OpcaoRespostaPergunta()
        {
            RespostaColeta = new HashSet<RespostaColeta>();
        }

        public int Id { get; set; }
        public int IdPergunta { get; set; }
        public string Opcao { get; set; }

        public Pergunta IdPerguntaNavigation { get; set; }
        public ICollection<RespostaColeta> RespostaColeta { get; set; }
    }
}

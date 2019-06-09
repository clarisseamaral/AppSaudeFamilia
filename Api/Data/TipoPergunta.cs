using System;
using System.Collections.Generic;

namespace ColetaApi.Data
{
    public partial class TipoPergunta
    {
        public TipoPergunta()
        {
            Pergunta = new HashSet<Pergunta>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }

        public ICollection<Pergunta> Pergunta { get; set; }
    }
}

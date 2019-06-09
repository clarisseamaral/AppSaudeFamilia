using System;
using System.Collections.Generic;

namespace ColetaApi.Data
{
    public partial class Questionario
    {
        public Questionario()
        {
            Coleta = new HashSet<Coleta>();
            QuestionarioPergunta = new HashSet<QuestionarioPergunta>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }

        public ICollection<Coleta> Coleta { get; set; }
        public ICollection<QuestionarioPergunta> QuestionarioPergunta { get; set; }
    }
}

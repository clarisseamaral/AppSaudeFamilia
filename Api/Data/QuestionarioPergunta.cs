using System;
using System.Collections.Generic;

namespace ColetaApi.Data
{
    public partial class QuestionarioPergunta
    {
        public int IdQuestionario { get; set; }
        public int IdPergunta { get; set; }

        public Pergunta IdPerguntaNavigation { get; set; }
        public Questionario IdQuestionarioNavigation { get; set; }
    }
}

using Coleta.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Coleta.ViewsModel
{
    public class CadastroPergunta
    {
        [Display(Name = "Tipo da pergunta")]
        public List<TipoPergunta> TiposPergunta { get; set; }

        public Pergunta Pergunta { get; set; }

        [Display(Name = "Opções de resposta")]
        public List<OpcaoRespostaPergunta> OpcaoRespostaPergunta { get; set; }
    }
}
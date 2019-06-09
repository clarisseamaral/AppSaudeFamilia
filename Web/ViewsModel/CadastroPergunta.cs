using Coleta.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Coleta.ViewsModel
{
    public class CadastroPergunta
    {
        [Display(Name = "Tipo da pergunta")]
        public IList<ColetaApi.Models.TipoPerguntaDto> TiposPergunta { get; set; }

        public ColetaApi.Models.PerguntaDto Pergunta { get; set; }

        [Display(Name = "Opções de resposta")]
        public List<OpcaoRespostaPergunta> OpcaoRespostaPergunta { get; set; }
    }
}
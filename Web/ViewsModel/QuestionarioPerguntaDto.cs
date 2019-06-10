using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Coleta.ViewsModel
{
    public class QuestionarioPerguntaDto
    {
        [Key]
        public int IdQuestionarioPergunta { get; set; }

        [Required]
        [StringLength(500)]
        public string Descricao { get; set; }

        public IList<PerguntaItemDto> Perguntas { get; set; }

        [Required]
        public IList<string> SelectedPerguntas { get; set; }
    }

    public class PerguntaItemDto
    {
        [Key]
        public int IdPergunta { get; set; }

        public string Descricao { get; set; }
    }

}
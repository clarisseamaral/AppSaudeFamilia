namespace Coleta.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RespostaColeta")]
    public partial class RespostaColeta
    {
        public int id { get; set; }

        public int idPergunta { get; set; }

        public int? idOpcaoResposta { get; set; }

        public int? valorResposta { get; set; }

        public virtual OpcaoRespostaPergunta OpcaoRespostaPergunta { get; set; }

        public virtual Pergunta Pergunta { get; set; }
    }
}

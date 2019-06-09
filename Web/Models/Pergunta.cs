namespace Coleta.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Pergunta")]
    public partial class Pergunta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pergunta()
        {
            OpcaoRespostaPerguntas = new HashSet<OpcaoRespostaPergunta>();
            RespostaColetas = new HashSet<RespostaColeta>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Tipo da pergunta")]
        public int IdTipoPergunta { get; set; }

        [Display(Name = "Ativo")]
        public bool FlgAtivo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [Display(Name = "Opções de resposta")]
        public virtual ICollection<OpcaoRespostaPergunta> OpcaoRespostaPerguntas { get; set; }

        [Display(Name = "Tipo da pergunta")]
        public virtual TipoPergunta TipoPergunta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RespostaColeta> RespostaColetas { get; set; }
    }
}

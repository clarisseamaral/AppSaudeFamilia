namespace Coleta.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TipoPergunta")]
    public partial class TipoPergunta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipoPergunta()
        {
            Perguntas = new HashSet<Pergunta>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Tipo da pergunta")]
        public string descricao { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pergunta> Perguntas { get; set; }
    }
}

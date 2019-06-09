namespace Coleta.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OpcaoRespostaPergunta")]
    public partial class OpcaoRespostaPergunta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OpcaoRespostaPergunta()
        {
            RespostaColetas = new HashSet<RespostaColeta>();
        }

        public int id { get; set; }

        public int idPergunta { get; set; }

        [Required]
        [StringLength(100)]
        public string opcao { get; set; }

        public virtual Pergunta Pergunta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RespostaColeta> RespostaColetas { get; set; }
    }
}

namespace Coleta.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Usuario")]
    public partial class Usuario
    {
        public int id { get; set; }

        public int idColetor { get; set; }

        [Required]
        [StringLength(100)]
        public string login { get; set; }

        [Required]
        [StringLength(100)]
        public string senha { get; set; }

        public bool acessoAdministrador { get; set; }

        public virtual Coletor Coletor { get; set; }
    }
}

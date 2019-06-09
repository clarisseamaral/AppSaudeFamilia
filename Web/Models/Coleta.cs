namespace Coleta.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Coleta")]
    public partial class Coleta
    {
        public int id { get; set; }

        public int idColetor { get; set; }

        public DateTime data { get; set; }

        public int latitude { get; set; }

        public int longetude { get; set; }

        public virtual Coletor Coletor { get; set; }
    }
}

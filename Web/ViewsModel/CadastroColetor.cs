using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Coleta.ViewsModel
{
    public class CadastroColetor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

    }
}
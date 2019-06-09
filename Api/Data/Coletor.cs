using System;
using System.Collections.Generic;

namespace ColetaApi.Data
{
    public partial class Coletor
    {
        public Coletor()
        {
            Coleta = new HashSet<Coleta>();
            Usuario = new HashSet<Usuario>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }

        public ICollection<Coleta> Coleta { get; set; }
        public ICollection<Usuario> Usuario { get; set; }
    }
}

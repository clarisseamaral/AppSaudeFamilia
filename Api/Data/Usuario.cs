using System;
using System.Collections.Generic;

namespace ColetaApi.Data
{
    public partial class Usuario
    {
        public Usuario()
        {
            Coleta = new HashSet<Coleta>();
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public bool AcessoAdministrador { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        public ICollection<Coleta> Coleta { get; set; }
    }
}

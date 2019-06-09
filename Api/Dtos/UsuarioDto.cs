using ColetaApi.Data;
using System.Runtime.Serialization;

namespace ColetaApi.Dtos
{
    public class UsuarioDto
    {
        public UsuarioDto(Usuario usuario)
        {
            Id = usuario.Id;
            Nome = usuario.Nome;
            Senha = usuario.Senha;
            Cpf = usuario.Cpf;
            Email = usuario.Email;
            Telefone = usuario.Telefone;
            AcessoAdministrador = usuario.AcessoAdministrador;
            Login = usuario.Login;
        }

        [DataMember]
        public string Descricao { get; set; }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Login { get; set; }

        [DataMember]
        public string Senha { get; set; }

        [DataMember]
        public bool AcessoAdministrador { get; set; }

        [DataMember]
        public string Nome { get; set; }

        [DataMember]
        public string Cpf { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Telefone { get; set; }
    }
}

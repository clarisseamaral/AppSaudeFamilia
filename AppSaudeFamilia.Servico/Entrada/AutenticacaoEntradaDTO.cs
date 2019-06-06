using System.Runtime.Serialization;

namespace AppSaudeFamilia.Servico
{
    [DataContract]
    public class AutenticacaoEntradaDTO
    {
        [DataMember(Name = "usuario")]
        public string Usuario { get; set; }

        [DataMember(Name = "senha")]
        public string Senha { get; set; }
    }
}
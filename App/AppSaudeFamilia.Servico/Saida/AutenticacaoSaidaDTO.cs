using System.Runtime.Serialization;

namespace AppSaudeFamilia.Servico
{
    [DataContract]
    public class AutenticacaoSaidaDTO
    {
        [DataMember(Name = "token")]
        public string Token { get; set; }
    }
}
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AppSaudeFamilia.Servico
{
    [DataContract]
    public class ListaPerguntasSaidaDTO
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "descricao")]
        public string Descricao { get; set; }

        [DataMember(Name = "idTipoPergunta")]
        public int IdTipoPergunta { get; set; }

        [DataMember(Name = "tipoPergunta")]
        public string TipoPergunta { get; set; }

        [DataMember(Name = "alternativas")]
        public List<Alternativa> Alternativas { get; set; }

        public string Resposta { get; set; }

        public int IdAlternativa { get; set; }
    }

    [DataContract]
    public class Alternativa
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "texto")]
        public string Texto { get; set; }
    }


}

using System;
using System.Collections.Generic;

namespace ColetaApi.Data
{
    public partial class Coleta
    {
        public Coleta()
        {
            RespostaColeta = new HashSet<RespostaColeta>();
        }

        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdQuestionario { get; set; }
        public DateTime Data { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Questionario IdQuestionarioNavigation { get; set; }
        public Usuario IdUsuarioNavigation { get; set; }
        public ICollection<RespostaColeta> RespostaColeta { get; set; }
    }
}

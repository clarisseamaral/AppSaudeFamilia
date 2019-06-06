using System.Data;
using System.Text;
using AppSaudeFamilia.Util;

namespace AppSaudeFamilia.DataLocal
{
    public class QuestionarioDB : IDataLocal
    {
        public int IdQuestionario { get; set; }
        public int IdPergunta { get; set; }
        public int IdResposta { get; set; }
        public string Resposta { get; set; }
        public string Data { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public QuestionarioDB(DataRow dr)
        {
            ConvertDr(dr);
        }

        public QuestionarioDB() { }

        public static string TableColumns {
            get {
                return @"IdQuestionario INTEGER, 
                         IdPergunta INTEGER, 
                         IdResposta INTEGER, 
                         Resposta ntext, 
                         Data ntext,
                         Longitude ntext, 
                         Latitude ntext";
            }
        }


        public static string TableName {
            get { return "Questionario"; }
        }

        public string InsertQuery {
            get {
                var sb = new StringBuilder();
                sb.AppendFormat("insert into {0} (IdQuestionario, IdPergunta, IdResposta, Resposta, Data, Longitude, Latitude)", TableName);

                sb.Append(" values(");
                sb.AppendFormat("'{0}',", this.IdQuestionario);
                sb.AppendFormat("'{0}',", this.IdPergunta);
                sb.AppendFormat("'{0}',", this.IdResposta);
                sb.AppendFormat("'{0}',", this.Resposta);
                sb.AppendFormat("'{0}',", this.Data);
                sb.AppendFormat("'{0}',", this.Longitude);
                sb.AppendFormat("'{0}'", this.Latitude);

                sb.Append(")");

                return sb.ToString();
            }
        }

        public string UpdateQuery => throw new System.NotImplementedException();

        public string SelectQuery {
            get {
                var sb = new StringBuilder();
                sb.AppendFormat("SELECT * from {0} ", TableName);
                return sb.ToString();
            }
        }

        public void ConvertDr(DataRow dr)
        {
            int idQuestionario; int.TryParse(dr["IdQuestionario"].ToString(), out idQuestionario);
            IdQuestionario = idQuestionario;

            int idPergunta; int.TryParse(dr["IdPergunta"].ToString(), out idPergunta);
            IdPergunta = idPergunta;

            int idResposta; int.TryParse(dr["IdResposta"].ToString(), out idResposta);
            IdResposta = idResposta;

            Resposta = dr["Resposta"].ToString();
            Data = dr["Data"].ToString();
            Longitude = dr["Longitude"].ToString();
            Latitude = dr["Latitude"].ToString();
        }
    }
}
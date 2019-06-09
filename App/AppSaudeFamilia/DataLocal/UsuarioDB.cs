using AppSaudeFamilia.Util;
using System.Data;
using System.Text;

namespace AppSaudeFamilia.DataLocal
{
    public class UsuarioDB : IDataLocal
    {
        public string Token { get; set; }

        public UsuarioDB(DataRow dr)
        {
            ConvertDr(dr);
        }

        public UsuarioDB() { }

        public UsuarioDB(string token) { Token = token; }

        public static string TableColumns {
            get {
                return @" Token ntext ";
            }
        }

        public string SelectQuery {
            get {
                var sb = new StringBuilder();
                sb.AppendFormat("SELECT * from {0} ", TableName);
                return sb.ToString();
            }
        }

        public static string TableName {
            get { return "Usuario"; }
        }

        public string InsertQuery {
            get {
                var sb = new StringBuilder();
                sb.AppendFormat("insert into {0} (Token)", TableName);

                sb.Append(" values(");
                sb.AppendFormat("'{0}'", this.Token);
                sb.Append(")");

                return sb.ToString();
            }
        }


        public string UpdateQuery {
            get {
                var sb = new StringBuilder();
                sb.AppendFormat("update {0} ", TableName);
                sb.AppendFormat("set {0} = '{1}'", "Token", Token);
                return sb.ToString();
            }
        }

        public void ConvertDr(DataRow dr)
        {
            Token = dr["Token"].ToString();
        }
    }
}
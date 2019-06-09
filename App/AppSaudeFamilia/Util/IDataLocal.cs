using System.Data;

namespace AppSaudeFamilia.Util
{
    public interface IDataLocal
    {
        string InsertQuery { get; }

        string UpdateQuery { get; }

        void ConvertDr(DataRow dr);
    }
}

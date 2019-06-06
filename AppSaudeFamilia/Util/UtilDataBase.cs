using Android.App;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using Mono.Data.Sqlite;
using System.Threading.Tasks;

namespace AppSaudeFamilia.Util
{
    public static class UtilDataBase
    {
        public static string PathDataBase {
            get {

                //Banco original do app
                var docsFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                var pathToDatabase = System.IO.Path.Combine(docsFolder, "BancoLocal.db");
                return pathToDatabase;

                //todo: voltar para o banco original
                ////Banco para testes
                //var teste = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments).AbsolutePath;
                //var teste2 = System.IO.Path.Combine(teste, "BancoLocalTeste.db");
                //return teste2;

            }
        }

        public static string ConnectionString {
            get {
                return string.Format("Data Source={0};Version=3;", PathDataBase);
            }
        }

        public static bool CreateDataBase()
        {
            bool returnValue = false;
            try
            {
                if (!File.Exists(PathDataBase))
                {
                    SqliteConnection.CreateFile(PathDataBase);
                }

                returnValue = true;
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Erro ao criar banco!");
            }

            return returnValue;
        }

        public static bool CreateTable(string nameTable, string columns)
        {
            if (!TableExist(nameTable))
            {
                using (var conn = new SqliteConnection((ConnectionString)))
                {
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = string.Format("CREATE TABLE {0} ({1})", nameTable, columns);
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool TableExist(string nameTable)
        {
            bool returnValue = false;

            try
            {
                using (var conn = new SqliteConnection((ConnectionString)))
                {
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = string.Format("SELECT count(*) FROM sqlite_master WHERE type = 'table' AND name = '{0}'", nameTable);
                        command.CommandType = CommandType.Text;
                        var obj = command.ExecuteScalar();

                        if (obj is long)
                        {
                            returnValue = ((long)obj) > 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Erro to verify table!");
            }

            return returnValue;
        }

        public static int CountItem(string nameTable, string filtro = null)
        {
            int returnValue = 0;

            try
            {
                using (var conn = new SqliteConnection((ConnectionString)))
                {
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        string query = string.Format("SELECT count(*) FROM {0}", nameTable);
                        if (!string.IsNullOrWhiteSpace(filtro))
                        {
                            query += string.Format(" where {0}", filtro);
                        }

                        command.CommandText = query;
                        command.CommandType = CommandType.Text;
                        var obj = command.ExecuteScalar();

                        if (obj is long)
                        {
                            returnValue = (int)((long)obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Erro ao verificar item da tabela!");
            }

            return returnValue;
        }

        public static int PegaUltimoItem(string nameTable, string filtro = null)
        {
            int returnValue = 0;

            try
            {
                using (var conn = new SqliteConnection((ConnectionString)))
                {
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        string query = string.Format("SELECT max(Id) FROM {0}", nameTable);
                        if (!string.IsNullOrWhiteSpace(filtro))
                        {
                            query += string.Format(" where {0}", filtro);
                        }

                        command.CommandText = query;
                        command.CommandType = CommandType.Text;
                        var obj = command.ExecuteScalar();

                        if (obj is long)
                        {
                            returnValue = (int)((long)obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Erro ao verificar item da tabela!");
            }

            return returnValue;
        }

        public static async Task<int> CountItemAsync(string nameTable, string filtro = null)
        {
            int returnValue = 0;

            try
            {
                using (var conn = new SqliteConnection((ConnectionString)))
                {
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        string query = string.Format("SELECT count(*) FROM {0}", nameTable);
                        if (!string.IsNullOrWhiteSpace(filtro))
                        {
                            query += string.Format(" where {0}", filtro);
                        }

                        command.CommandText = query;
                        command.CommandType = CommandType.Text;
                        var obj = await command.ExecuteScalarAsync();

                        if (obj is long)
                        {
                            returnValue = (int)((long)obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Erro para acessar o banco local, contagem itens!");
            }

            return returnValue;
        }

        public async static Task SaveAsync(string query)
        {

            using (var conn = new SqliteConnection((ConnectionString)))
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public static void Save(string query)
        {

            using (var conn = new SqliteConnection((ConnectionString)))
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }

        public async static Task UpdateAsync(string query)
        {

            using (var conn = new SqliteConnection((ConnectionString)))
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public static void UpdateSave(string query)
        {

            using (var conn = new SqliteConnection((ConnectionString)))
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }

        public async static Task DeleAsyncQuery(string query)
        {
            using (var conn = new SqliteConnection((ConnectionString)))
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = query;

                    command.CommandType = CommandType.Text;
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async static Task DeleteAsync(string nameTable, int id = 0)
        {
            using (var conn = new SqliteConnection((ConnectionString)))
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    if (id > 0)
                    {
                        command.CommandText = string.Format("delete from {0} where Id = {1}", nameTable, id);
                    }
                    else
                    {
                        command.CommandText = string.Format("delete from {0}", nameTable);
                    }

                    command.CommandType = CommandType.Text;
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public static void DeleteString(string query)
        {
            using (var conn = new SqliteConnection((ConnectionString)))
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = query;

                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void Delete(string nameTable, int id = 0)
        {
            using (var conn = new SqliteConnection((ConnectionString)))
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    if (id > 0)
                    {
                        command.CommandText = string.Format("delete from {0} where Id = {1}", nameTable, id);
                    }
                    else
                    {
                        command.CommandText = string.Format("delete from {0}", nameTable);
                    }

                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void Delete(string tableName, string filtro)
        {
            using (var conn = new SqliteConnection((ConnectionString)))
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = string.Format("delete from {0} {1}", tableName, filtro);
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }

        public static DataTable GetItems(string nameTable, string filtro = null, string ordernacao = null)
        {
            DataTable dataTableResult = new DataTable();

            using (var conn = new SqliteConnection((ConnectionString)))
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    string query = string.Format("select * from {0}", nameTable);
                    if (!string.IsNullOrWhiteSpace(filtro))
                    {
                        query += string.Format(" where {0} ", filtro);
                    }

                    if (!string.IsNullOrWhiteSpace(ordernacao))
                    {
                        query += (" " + ordernacao);
                    }

                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    using (SqliteDataReader rdr = command.ExecuteReader())
                    {
                        dataTableResult.Load(rdr);
                    }
                }
            }

            return dataTableResult;
        }

        public static async Task<DataTable> GetItemsAsync(string nameTable, string filtro = null, string ordernacao = null)
        {
            DataTable dataTableResult = new DataTable();

            using (var conn = new SqliteConnection((ConnectionString)))
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    string query = string.Format("select * from {0}", nameTable);
                    if (!string.IsNullOrWhiteSpace(filtro))
                    {
                        query += string.Format(" where {0} ", filtro);
                    }

                    if (!string.IsNullOrWhiteSpace(ordernacao))
                    {
                        query += (" " + ordernacao);
                    }

                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    using (var rdr = await command.ExecuteReaderAsync())
                    {
                        dataTableResult.Load(rdr);
                    }
                }
            }

            return dataTableResult;
        }

        public static IList<T> GetItems<T>(string nameTable, Action<DataTable, IList<T>> conversor, string filtro = null, string ordernacao = null)
        {
            var result = Activator.CreateInstance<List<T>>();
            DataTable dataTableResult = new DataTable();

            using (var conn = new SqliteConnection((ConnectionString)))
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    string query = string.Format("select * from {0}", nameTable);
                    if (!string.IsNullOrWhiteSpace(filtro))
                    {
                        query += string.Format(" where {0} ", filtro);
                    }

                    if (!string.IsNullOrWhiteSpace(ordernacao))
                    {
                        query += (" " + ordernacao);
                    }

                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    using (SqliteDataReader rdr = command.ExecuteReader())
                    {
                        dataTableResult.Load(rdr);
                    }
                }
            }

            conversor.Invoke(dataTableResult, result);

            return result;
        }

        public static DataTable GetItemsQuery(string query)
        {
            DataTable dataTableResult = new DataTable();

            using (var conn = new SqliteConnection((ConnectionString)))
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    using (SqliteDataReader rdr = command.ExecuteReader())
                    {
                        dataTableResult.Load(rdr);
                    }
                }
            }
            return dataTableResult;
        }

        public static IList<T> GetItems<T>(string query, Action<DataTable, IList<T>> conversor)
        {
            var result = Activator.CreateInstance<List<T>>();
            DataTable dataTableResult = new DataTable();

            using (var conn = new SqliteConnection((ConnectionString)))
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    using (SqliteDataReader rdr = command.ExecuteReader())
                    {
                        dataTableResult.Load(rdr);
                    }
                }
            }

            conversor.Invoke(dataTableResult, result);

            return result;
        }

        public static T GetItem<T>(string query, Action<DataRow, T> conversor)
        {
            var result = Activator.CreateInstance<T>();
            DataTable dataTableResult = new DataTable();

            using (var conn = new SqliteConnection((ConnectionString)))
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    using (SqliteDataReader rdr = command.ExecuteReader())
                    {
                        dataTableResult.Load(rdr);
                    }
                }
            }

            if (dataTableResult.Rows.Count > 0)
            {
                conversor.Invoke(dataTableResult.Rows[0], result);
            }
            else
            {
                result = default(T);
            }

            return result;
        }

        public static T GetItem<T>(string nameTable, string filtro, string ordernacao, Action<DataRow, T> conversor)
        {
            var result = Activator.CreateInstance<T>();
            DataTable dataTableResult = new DataTable();

            using (var conn = new SqliteConnection((ConnectionString)))
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    string query = string.Format("select * from {0}", nameTable);
                    if (!string.IsNullOrWhiteSpace(filtro))
                    {
                        query += string.Format(" where {0} ", filtro);
                    }

                    query += " limit 1";

                    if (!string.IsNullOrWhiteSpace(ordernacao))
                    {
                        query += (" " + ordernacao);
                    }

                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    using (SqliteDataReader rdr = command.ExecuteReader())
                    {
                        dataTableResult.Load(rdr);
                    }
                }
            }

            if (dataTableResult.Rows.Count > 0)
            {
                conversor.Invoke(dataTableResult.Rows[0], result);
            }
            else
            {
                result = default(T);
            }

            return result;
        }

        public static T GetItem<T>(string nameTable, string filtro, Action<DataRow, T> conversor)
        {
            return GetItem<T>(nameTable, filtro, "", conversor);
        }


        #region Busca 1 item já covertido

        public static T GetItemDireto<T>(string query) where T : IDataLocal
        {
            Action<DataRow, T> conversor = (dr, item) => { item.ConvertDr(dr); };
            return GetItem<T>(query, conversor);
        }

        public static T GetItemDireto<T>(string nameTable, string filtro, string ordernacao) where T : IDataLocal
        {
            Action<DataRow, T> conversor = (dr, item) => { item.ConvertDr(dr); };
            return GetItem<T>(nameTable, filtro, ordernacao, conversor);
        }

        public static T GetItemDireto<T>(string nameTable, string filtro) where T : IDataLocal
        {
            Action<DataRow, T> conversor = (dr, item) => { item.ConvertDr(dr); };
            return GetItem<T>(nameTable, filtro, "", conversor);
        }

        #endregion
    }
}
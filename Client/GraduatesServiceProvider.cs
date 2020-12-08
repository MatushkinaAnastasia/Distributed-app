using Common.Model;
using System.Collections.Generic;
using System.Collections;
using System.Data.SQLite;

namespace Client
{
    public class GraduatesServiceProvider
    {
        private readonly string _connectionString;

        public GraduatesServiceProvider(string path)
        {
            _connectionString = new SQLiteConnectionStringBuilder
            {
                DataSource = path,
            }.ConnectionString;
        }

        public IEnumerable GetAll(int sliceSize)
        {
            using var conn = new SQLiteConnection(_connectionString);
            var sCommand = new SQLiteCommand()
            {
                Connection = conn,
                CommandText = @"SELECT fio, name_faculty, name_university, name_subject, grade FROM graduates;"
            };

            conn.Open();
            var reader = sCommand.ExecuteReader();

            var result = new List<Graduate>();

            while (reader.Read())
            {
                string fio = (string)reader["fio"];
                string name_faculty = (string)reader["name_faculty"];
                string name_university = (string)reader["name_university"];
                string name_subject = (string)reader["name_subject"];
                int grade = (int)reader["grade"];
                result.Add(new Graduate(fio, name_faculty, name_university, name_subject, grade));

                if (result.Count == sliceSize)
                {
                    yield return result;
                    result.Clear();
                }
            }

            if (result.Count > 0)
            {
                yield return result;
            }
        }
    }
}

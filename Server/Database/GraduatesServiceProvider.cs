using Npgsql;

namespace Server.Database
{
	public class GraduatesServiceProvider
    {
        private readonly string connectionString;

        public GraduatesServiceProvider(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int InsertUniversity(string name_university)
        {
            using var sConn = new NpgsqlConnection(connectionString);
            sConn.Open();

            var query = @"select id_university from university where name_university = @name_university;";
            var command = new NpgsqlCommand(query, sConn);
            command.Parameters.AddWithValue("@name_university", name_university);

            var id = (int?)command.ExecuteScalar();
            if (id is null)
            {
                var queryInsert = @"insert into university (name_university) values (@name_university) returning id_university";
                var commandInsert = new NpgsqlCommand(queryInsert, sConn);
                commandInsert.Parameters.AddWithValue("@name_university", name_university);
                id = (int)commandInsert.ExecuteScalar();
            }
            return (int)id;
        }

        public int InsertFaculty(string name_faculty, int id_university)
        {
            using var sConn = new NpgsqlConnection(connectionString);
            sConn.Open();

            var query = @"select id_faculty from faculty where name_faculty = @name_faculty and id_university = @id_university;";
            var command = new NpgsqlCommand(query, sConn);
            command.Parameters.AddWithValue("@name_faculty", name_faculty);
            command.Parameters.AddWithValue("@id_university", id_university);

            var id = (int?)command.ExecuteScalar();
            if (id is null)
            {
                var queryInsert = @"insert into faculty (name_faculty, id_university) 
values (@name_faculty, @id_university) returning id_faculty";
                var commandInsert = new NpgsqlCommand(queryInsert, sConn);
                commandInsert.Parameters.AddWithValue("@name_faculty", name_faculty);
                commandInsert.Parameters.AddWithValue("@id_university", id_university);
                id = (int)commandInsert.ExecuteScalar();
            }
            return (int)id;
        }

        public int InsertStudent(string fio, int id_faculty)
        {
            using var sConn = new NpgsqlConnection(connectionString);
            sConn.Open();

            var query = @"select id_student from student where fio = @fio;";
            var command = new NpgsqlCommand(query, sConn);
            command.Parameters.AddWithValue("@fio", fio);

            var id = (int?)command.ExecuteScalar();
            if (id is null)
            {
                var queryInsert = @"insert into student (fio, id_faculty) values (@fio, @id_faculty) returning id_student";
                var commandInsert = new NpgsqlCommand(queryInsert, sConn);
                commandInsert.Parameters.AddWithValue("@fio", fio);
                commandInsert.Parameters.AddWithValue("@id_faculty", id_faculty);
                id = (int)commandInsert.ExecuteScalar();
            }
            return (int)id;
        }

        public int InsertSubject(string name_subject)
        {
            using var sConn = new NpgsqlConnection(connectionString);
            sConn.Open();

            var query = @"select id_subject from subject where name_subject = @name_subject;";
            var command = new NpgsqlCommand(query, sConn);
            command.Parameters.AddWithValue("@name_subject", name_subject);

            var id = (int?)command.ExecuteScalar();
            if (id is null)
            {
                var queryInsert = @"insert into subject (name_subject) values (@name_subject) returning id_subject";
                var commandInsert = new NpgsqlCommand(queryInsert, sConn);
                commandInsert.Parameters.AddWithValue("@name_subject", name_subject);
                id = (int)commandInsert.ExecuteScalar();
            }
            return (int)id;
        }

        public int InsertGrade(int grade, int id_student, int id_subject)
        {
            using var sConn = new NpgsqlConnection(connectionString);
            sConn.Open();

            var query = @"select id_note from grade_subject where grade = @grade and id_student = @id_student and id_subject = @id_subject;";
            var command = new NpgsqlCommand(query, sConn);
            command.Parameters.AddWithValue("@grade", grade);
            command.Parameters.AddWithValue("@id_student", id_student);
            command.Parameters.AddWithValue("@id_subject", id_subject);

            var id = (int?)command.ExecuteScalar();
            if (id is null)
            {
                var queryInsert = @"insert into grade_subject (id_student, id_subject, grade) 
values (@id_student, @id_subject, @grade) returning id_note";
                var commandInsert = new NpgsqlCommand(queryInsert, sConn);
                commandInsert.Parameters.AddWithValue("@id_student", id_student);
                commandInsert.Parameters.AddWithValue("@id_subject", id_subject);
                commandInsert.Parameters.AddWithValue("@grade", grade);
                id = (int)commandInsert.ExecuteScalar();
            }
            return (int)id;
        }
    }
}

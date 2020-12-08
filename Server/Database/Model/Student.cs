namespace Server.Database.Model
{
    public class Student
    {
        public Student(string fio, int id_student, int id_faculty)
        {
            Fio = fio;
            Id_student = id_student;
            Id_faculty = id_faculty;
        }

        public string Fio { get; set; }
        public int Id_student { get; set; }

        public int Id_faculty { get; set; }
    }
}

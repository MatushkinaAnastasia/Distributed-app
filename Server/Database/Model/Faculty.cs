namespace Server.Database.Model
{
    public class Faculty
    {
        public Faculty(string name_faculty, int id_university, int id_faculty)
        {
            Name_faculty = name_faculty;
            Id_university = Id_university;
            Id_faculty = id_faculty;
        }

        public string Name_faculty { get; set; }
        public int Id_university { get; set; }

        public int Id_faculty { get; set; }
    }
}


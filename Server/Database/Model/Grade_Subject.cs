using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Database.Model
{
	class Grade_Subject
	{
        public Grade_Subject(int id_note, int id_student, int id_subject, int grade)
        {
            Id_student = id_student;
            Id_note = id_note;
            Id_Subject = id_subject;
            Grade = grade;
        }

        public int Id_note { get; set; }
        public int Id_student { get; set; }
        public int Id_Subject { get; set; }
        public int Grade { get; set; }
    }
}

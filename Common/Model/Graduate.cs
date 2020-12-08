using System;

namespace Common.Model
{
	public class Graduate
    {
        public Graduate(string fio, string name_faculty, string name_university, string name_subject, int grade)
        {
            Fio = fio ?? throw new ArgumentNullException(nameof(fio));
            Name_faculty = name_faculty ?? throw new ArgumentNullException(nameof(name_faculty));
            Name_university = name_university ?? throw new ArgumentNullException(nameof(name_university));
            Name_subject = name_subject ?? throw new ArgumentNullException(nameof(name_subject));
            Grade = grade;
        }

        public string Fio { get; set; }

        public string Name_faculty { get; set; }

        public string Name_university { get; set; }

        public string Name_subject { get; set; }

        public int Grade { get; set; }
    }
}

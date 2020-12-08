using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Database.Model
{
	class Subject
	{
		public Subject (string name_subject, int id_subject)
		{
			Name_subject = name_subject;
			Id_subject = id_subject;
		}
		public string Name_subject { get; set; }
		public int Id_subject{ get; set; }
	}
}

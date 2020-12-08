using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Database.Model
{
	class Universty
	{
		public Universty(string name_university, int id_university)
		{
			Name_university = name_university;
			Id_university = id_university;
		}
		public string Name_university { get; set; }
		public int Id_university { get; set; }
	}
}

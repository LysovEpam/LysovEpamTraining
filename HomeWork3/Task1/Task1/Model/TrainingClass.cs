using System;
using System.Collections.Generic;

namespace Task1.Model
{
	class TrainingClass
	{
		public List<Student> Students { get; private set; }
		public Teacher Teacher { get; private set; }
		public string ClassName { get; private set; }


		public TrainingClass()
		{
			
		}
		public TrainingClass(List<Student> students, Teacher teacher, string className)
		{
			Students = students;
			Teacher = teacher;
			ClassName = className;
		}

		public void ChangeClassName(string newClassName)
		{
			if (string.IsNullOrEmpty(newClassName))
				throw new ArgumentException("Class name cannot be empty", nameof(newClassName));

			ClassName = newClassName;
		}

		public void ChangeTeacher(Teacher newTeacher)
		{
			if(newTeacher == null)
				throw new ArgumentException("Teacher cannot be empty", nameof(newTeacher));

			Teacher = newTeacher;
		}

		public void AddNewStudent(Student newStudent)
		{
			if (newStudent == null)
				throw new ArgumentException("Student cannot be empty", nameof(newStudent));

			Students.Add(newStudent);

		}

		public void RemoveStudent(Student removeStudent)
		{
			if (removeStudent == null)
				throw new ArgumentException("Student cannot be empty", nameof(removeStudent));

			Students.Remove(removeStudent);
		}

		public int GetCountStudent()
		{
			return Students.Count;
		}

		public void OneYearHasPassed()
		{
			Teacher.AddOneYearAge();

			foreach (Student student in Students)
			{
				
			}
		}

	}
}

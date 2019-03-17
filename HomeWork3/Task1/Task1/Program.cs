using System;
using System.Collections.Generic;
using System.Text;
using Task1.Model;


namespace Task1
{
	class Program
	{
		static void Main()
		{
			Console.OutputEncoding = Encoding.Unicode;

			List<Student> studentsListFirst = new List<Student>
			{
				new Student(2018, "Alex", 18, 92),
				new Student(2018, "Boris", 18, 85),
				new Student(2018, "Jack", 19, 75),
				new Student(2018, "Amelia", 18, 58),
				new Student(2018, "Lily", 19, 60)
			};

			Teacher firstTeacher = new Teacher("History", new DateTime(2011,5,1), 3500, "Gerald Hall", 35, 87);
			TrainingClass trainingClassFirst = new TrainingClass(studentsListFirst, firstTeacher, "History A51");

			PrintClassInformation(trainingClassFirst);

			#region Change entities for test

			studentsListFirst[0].SetWright(91);
			studentsListFirst[1].SetName("Oscar");	//Boris change name
			studentsListFirst[1].SetWright(89);     //Boris put on weight
			studentsListFirst[3].SetWright(59);

			foreach (Student student in studentsListFirst)
			{
				student.AddOneYearAge();
				student.IncreaseYearOfStudy();
			}


			firstTeacher.ChangeAcademicSubject("History and history of the English language");
			firstTeacher.AddSalary(500);
			firstTeacher.AddOneYearAge();

			#endregion

			Console.WriteLine("\nShow new information for test:\n");
			PrintClassInformation(trainingClassFirst);
			Console.ReadLine();
		}

		static void PrintClassInformation(TrainingClass trainingClass)
		{
			Console.WriteLine($"Class {trainingClass.ClassName} information ");
			Console.WriteLine($"Teacher information: \n{trainingClass.Teacher}");
			Console.WriteLine($"{trainingClass.GetCountStudent()} students in the class, students informations:");
			foreach (Student student in trainingClass.Students)
				Console.WriteLine($"Student: {student}");
			
		}

		
	}
}

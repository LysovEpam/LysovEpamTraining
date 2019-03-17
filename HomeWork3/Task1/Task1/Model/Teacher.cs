using System;

namespace Task1.Model
{
	class Teacher : Man
	{
		public enum MyEnum
		{
			
		}
		//Создать производный класс Teacher, разработать его функциональность самостоятельно.

		public string AcademicSubject { get; private set; }

		public double Salary { get; private set; }

		public DateTime CarierStart { get; private set; }

		public Teacher(string academicSubject, DateTime carierStart, double salary, string name, int age, double weight) : base(name, age, weight)
		{
			ChangeAcademicSubject(academicSubject);
			SetSalary(salary);
			ChangeCarierStart(carierStart);
		}

		public void ChangeAcademicSubject(string newAcademicSubject)
		{
			if (string.IsNullOrEmpty(newAcademicSubject))
				throw new ArgumentException("Academic subject cannot be empty", nameof(newAcademicSubject));

			AcademicSubject = newAcademicSubject;
		}

		public void ChangeCarierStart(DateTime carierStart)
		{
			DateTime minDateTimeStart = new DateTime(1900,1,1);
			DateTime maxDateTimeStart = DateTime.Now;

			if(carierStart>maxDateTimeStart || carierStart<minDateTimeStart)
				throw new ArgumentException($"Carier date start must be {minDateTimeStart}-{maxDateTimeStart}", nameof(carierStart));

			CarierStart = carierStart;
		}

		public void SetSalary(double newSalary)
		{
			int minSalary = 0;
			int maxSalary = 15000;

			if (Salary > maxSalary || Salary < minSalary)
				throw new ArgumentException($"Salary must be {minSalary}-{maxSalary}", nameof(newSalary));

			Salary = newSalary;
		}

		public void AddSalary(double value)
		{
			Salary+= value;
		}

		public int GetWorkExperienceMonths()
		{
			return new DateTime((DateTime.Now - CarierStart).Ticks).Month;
		}
		public int GetWorkExperienceYears()
		{
			return new DateTime((DateTime.Now - CarierStart).Ticks).Year;
		}

		public override string ToString()
		{
			return $"name: {Name}, age: {Age}, weight: {Weight}, academic subject: {AcademicSubject}, salary: {Salary}";
		}
	}
}

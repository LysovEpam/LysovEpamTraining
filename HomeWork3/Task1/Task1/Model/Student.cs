

using System;

namespace Task1.Model
{
	class Student : Man
	{
		//Создать производный класс Student, имеющий поле года обучения.Определить методы задания и увеличения года обучения.

		public int YearOfStudy { get; private set; }


		
		public Student(int yearOfStudy, string name, int age, double weight) :base(name, age, weight)
		{
			SetYearOfStudy(yearOfStudy);
		}

		public void SetYearOfStudy(int yearOfStudy)
		{
			int minYear = 1900;
			int maxYear = 2100;

			if(yearOfStudy>maxYear || yearOfStudy < minYear)
				throw new ArgumentException($"Year of study must be {minYear}-{maxYear}", nameof(yearOfStudy));

			YearOfStudy = yearOfStudy;
		}

		public void IncreaseYearOfStudy()
		{
			YearOfStudy++;
		}

		public override string ToString()
		{
			return $"name {Name}, age: {Age}, weight: {Weight}, years of study: {YearOfStudy}";
		}
	}
}

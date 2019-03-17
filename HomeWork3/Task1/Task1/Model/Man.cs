using System;

namespace Task1.Model
{
	abstract class Man
	{
		//1) Создать класс Man (человек), с полями: имя, возраст, пол и вес. Определить методы задания имени, возраста и веса.

		public string Name { get; private set; }
		public int Age { get; private set; }
		public double Weight { get; private set; }

		#region Конструктор

		
		protected Man(string name, int age, double weight)
		{
			SetName(name);
			SetAge(age);
			SetWright(weight);
		}


		#endregion

		
		public void SetName(string name)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentException("Name cannot be empty", nameof(name));
			
			Name = name;
		}

		public void SetAge(int age)
		{
			if(age<0)
				throw new ArgumentException("Age can not be less than zero", nameof(age));

			int maxAge = 200; //for example

			if (age > maxAge)
				throw new ArgumentException($"age can not be more than {maxAge}", nameof(age));

			Age = age;
		}

		public void SetWright(double weight)
		{
			if (weight < 0)
				throw new ArgumentException("Weight can not be less than zero", nameof(weight));

			int maxWeight = 300; //for example

			if (weight > maxWeight)
				throw new ArgumentException($"Weight can not be more than {maxWeight}", nameof(weight));



			Weight = weight;
		}

		public void AddOneYearAge()
		{
			Age++;
		}
	}
}

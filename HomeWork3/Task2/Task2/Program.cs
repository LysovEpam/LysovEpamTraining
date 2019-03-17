using System;
using System.Collections.Generic;
using System.Text;
using Task2.Model;
using Task2.Model.Flowes;
using Task2.Model.MaterialsForBouquet;


namespace Task2
{
	class Program
	{
		static void Main()
		{
			Console.OutputEncoding = Encoding.Unicode;

			#region Test bouquet first

			List<Flower> bouquetFirst = new List<Flower>
			{
				new Rose(Rose.RoseVarietyEnum.GoldenGate, 5),
				new Rose(Rose.RoseVarietyEnum.Iceberg, 4),
				new Rose(Rose.RoseVarietyEnum.Pomponella, 5.5),

				new Carnation(Carnation.CarnationVarietyEnum.Grass, 3.35),

				new Gladiolus(Gladiolus.GladiolusVarietyEnum.Colville, 3),
				new Gladiolus(Gladiolus.GladiolusVarietyEnum.Kochi, 6),
				new Gladiolus(Gladiolus.GladiolusVarietyEnum.Harlem, 2),

				new Narcissus(Narcissus.NarcissusVarietyEnum.Triandrus, 4.75),

				new Tulip(Tulip.TulipVarietyEnum.Diana, 3),
				new Tulip(Tulip.TulipVarietyEnum.Diana, 3),
				new Tulip(Tulip.TulipVarietyEnum.Diana, 3),
				new Tulip(Tulip.TulipVarietyEnum.PinkTrophy, 2),
				new Tulip(Tulip.TulipVarietyEnum.RubyRed, 3.3)
			};

			double bouquetFirstCost = 0;
			Console.WriteLine("The first bouquet:");

			PrintCollection(bouquetFirst);

			foreach (Flower flower in bouquetFirst)
				bouquetFirstCost += flower.GetCost();
			
			Console.WriteLine($"Cost of first bouquet: {bouquetFirstCost}");


			#endregion
			#region Test bouquet second

			List<Flower> bouquetSecond = new List<Flower>
			{
				new Rose(Rose.RoseVarietyEnum.Iceberg, 4),
				new Rose(Rose.RoseVarietyEnum.Iceberg, 4),
				new Rose(Rose.RoseVarietyEnum.Iceberg, 4),
				new Rose(Rose.RoseVarietyEnum.Iceberg, 4),
				new Rose(Rose.RoseVarietyEnum.Iceberg, 4),

				new Rose(Rose.RoseVarietyEnum.Pomponella, 5.5),
				new Rose(Rose.RoseVarietyEnum.Pomponella, 5.5),
				new Rose(Rose.RoseVarietyEnum.Pomponella, 5.5),
				new Rose(Rose.RoseVarietyEnum.Pomponella, 5.5),
				new Rose(Rose.RoseVarietyEnum.Pomponella, 5.5),

				new Tulip(Tulip.TulipVarietyEnum.Diana, 3),
				new Tulip(Tulip.TulipVarietyEnum.Diana, 3),
				new Tulip(Tulip.TulipVarietyEnum.Diana, 3),
				new Tulip(Tulip.TulipVarietyEnum.Diana, 3),
				new Tulip(Tulip.TulipVarietyEnum.Diana, 3),
				new Tulip(Tulip.TulipVarietyEnum.Diana, 3),
				new Tulip(Tulip.TulipVarietyEnum.Diana, 3),
				new Tulip(Tulip.TulipVarietyEnum.Diana, 3),
				new Tulip(Tulip.TulipVarietyEnum.Diana, 3)
			};

			


			Packaging packaging = new Packaging(Packaging.PackagingTypeEnum.Film, 1);

			List<Decoration> decorations = new List<Decoration>
			{
				new Decoration(Decoration.DecorationsTypeEnum.Ribbon, 1),
				new Decoration(Decoration.DecorationsTypeEnum.Ribbon, 1),
				new Decoration(Decoration.DecorationsTypeEnum.Ribbon, 1),
				new Decoration(Decoration.DecorationsTypeEnum.SignBoard, 3)
			};

			BouquetOfFlowers bouquetOfFlowers = new BouquetOfFlowers(bouquetSecond, decorations, packaging);

			bouquetOfFlowers.AddFlower(new Carnation(Carnation.CarnationVarietyEnum.Garden, 5));

			Console.WriteLine("\n\nTest bouquet second\n");

			PrintCollection(bouquetOfFlowers.Flowers);
			PrintCollection(bouquetOfFlowers.Decorations);
			Console.WriteLine(bouquetOfFlowers.Packaging);
		
			Console.WriteLine($"Bouquet of flowers cost: {bouquetOfFlowers.GetCost()}");

			#endregion

			Console.ReadLine();
		}

		static void PrintCollection<T>(List<T> list)
		{
			foreach (T value in list)
				Console.WriteLine(value);
			
		}
	}
}

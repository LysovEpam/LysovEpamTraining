using System;
using System.Collections.Generic;
using Task2.Model.Flowes;
using Task2.Model.MaterialsForBouquet;

namespace Task2.Model
{
	class BouquetOfFlowers : ICostReceived
	{
		public List<Flower> Flowers { get; }

		public List<Decoration> Decorations { get; }

		public Packaging Packaging { get; }

		public BouquetOfFlowers(List<Flower> flowers, List<Decoration> decorations, Packaging packaging)
		{
			Flowers = flowers;
			Decorations = decorations;
			Packaging = packaging;
		}

		public void AddFlower(Flower flower)
		{
			if (flower == null)
				throw new ArgumentException($"Flower cannot be empty", nameof(flower));

			Flowers.Add(flower);
		}

		public void AddDecoration(Decoration decoration)
		{
			if (decoration == null)
				throw new ArgumentException($"Decoration cannot be empty", nameof(decoration));

			Decorations.Add(decoration);
		}



		public double GetCost()
		{
			double costResult = Packaging.GetCost();

			foreach (ICostReceived costReceived in Decorations)
				costResult += costReceived.GetCost();

			foreach (ICostReceived costReceived in Flowers)
				costResult += costReceived.GetCost();

			return costResult;
		}
	}
}

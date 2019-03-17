
using System;

namespace Task2.Model.Flowes
{
	abstract class Flower:ICostReceived
	{
		public double FlowerCost { get; private set; }

		protected Flower(double cost)
		{
			SetCost(cost);
		}

		public void SetCost(double cost)
		{
			double minCost = 0;
			double maxCost = 100000;

			if (cost > maxCost || cost < minCost)
				throw new ArgumentException($"Cost must be {minCost}-{maxCost}", nameof(cost));

			FlowerCost = cost;
		}

		public double GetCost()
		{
			return FlowerCost;
		}

		protected string GetFloweName(string floweName, string variety)
		{
			return $"Flower: {floweName}, variety: {variety}";
		}
	}
}

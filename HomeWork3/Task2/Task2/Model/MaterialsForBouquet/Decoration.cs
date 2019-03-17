namespace Task2.Model.MaterialsForBouquet
{
	class Decoration : ICostReceived
	{
		public enum DecorationsTypeEnum
		{
			Ribbon,
			DecorativeLeaves,
			BambooStick,
			SignBoard
		}

		public DecorationsTypeEnum DecorationsType { get; }

		public double DecorationCost { get; }

		public Decoration(DecorationsTypeEnum decorationsType, double decorationCost)
		{
			DecorationsType = decorationsType;
			DecorationCost = decorationCost;
		}


		public double GetCost()
		{
			return DecorationCost;
		}

		public override string ToString()
		{
			return $"Decoration: {DecorationsType}";
		}
	}
}

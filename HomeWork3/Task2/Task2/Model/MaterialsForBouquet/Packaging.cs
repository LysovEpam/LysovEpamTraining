namespace Task2.Model.MaterialsForBouquet
{
	class Packaging : ICostReceived
	{
		public enum PackagingTypeEnum
		{
			Film,
			Paper,
			Basket
		}

		public PackagingTypeEnum PackagingType { get; }

		public double PackagingCost { get; }

		public Packaging(PackagingTypeEnum packagingType, double packagingCost)
		{
			PackagingType = packagingType;
			PackagingCost = packagingCost;
		}


		public double GetCost()
		{
			return PackagingCost;
		}

		public override string ToString()
		{
			return $"Packaging: {PackagingType}";
		}
	}
}

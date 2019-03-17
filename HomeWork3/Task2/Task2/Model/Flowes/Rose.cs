namespace Task2.Model.Flowes
{
	class Rose : Flower
	{

		public enum RoseVarietyEnum
		{
			Pomponella,
			Flammentanz,
			Iceberg,
			GoldenGate
		}

		public RoseVarietyEnum RoseVariety { get; }

		public Rose(RoseVarietyEnum roseVariety, double cost):base(cost)
		{
			RoseVariety = roseVariety;
		}

		public override string ToString()
		{
			return GetFloweName(nameof(Rose), RoseVariety.ToString());
		}
	}
}

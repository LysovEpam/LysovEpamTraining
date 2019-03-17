namespace Task2.Model.Flowes
{
	class Carnation : Flower
	{
		public enum CarnationVarietyEnum
		{
			Garden,
			Turkish,
			Grass,
			Chinese
		}

		public CarnationVarietyEnum CarnationVariety { get; }

		public Carnation(CarnationVarietyEnum carnationVariety,double cost):base(cost)
		{
			CarnationVariety = carnationVariety;
		}

		public override string ToString()
		{
			return GetFloweName(nameof(Carnation), CarnationVariety.ToString());
		}

	}
}

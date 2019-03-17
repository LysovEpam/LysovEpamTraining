namespace Task2.Model.Flowes
{
	class Tulip : Flower
	{

		public enum TulipVarietyEnum
		{
			RubyRed,
			PrinceOfAustria,
			PinkTrophy,
			Flair,
			Diana
		}

		public TulipVarietyEnum TulipVariety { get; }

		public Tulip(TulipVarietyEnum tulipVariety, double cost) : base(cost)
		{
			TulipVariety = tulipVariety;
		}

		public override string ToString()
		{
			return GetFloweName(nameof(Tulip), TulipVariety.ToString());
		}
	}


}

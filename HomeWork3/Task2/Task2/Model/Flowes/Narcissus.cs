namespace Task2.Model.Flowes
{
	class Narcissus : Flower
	{

		public enum NarcissusVarietyEnum
		{
			Trumpet,
			LargeCupped,
			SmallCupped,
			Double,
			Triandrus
		}

		public NarcissusVarietyEnum NarcissusVariety { get; }

		public Narcissus(NarcissusVarietyEnum narcissusVariety, double cost) : base(cost)
		{
			NarcissusVariety = narcissusVariety;
		}

		public override string ToString()
		{
			return GetFloweName(nameof(Narcissus), NarcissusVariety.ToString());
		}
	}


}

namespace Task2.Model.Flowes
{
	class Gladiolus : Flower
	{
		public enum GladiolusVarietyEnum
		{
			Byzantine,
			Kochi,
			Colville,
			Harlem
		}

		public GladiolusVarietyEnum GladiolusVariety { get; }

		public Gladiolus(GladiolusVarietyEnum gladiolusVariety, double cost) : base(cost)
		{
			GladiolusVariety = gladiolusVariety;
		}

		public override string ToString()
		{
			return GetFloweName(nameof(Tulip), GladiolusVariety.ToString());
		}
	}
}

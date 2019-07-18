namespace CommonEntities
{
	public abstract class BaseDbEntity 
	{
		
		public int? IdEntity { get; set; }

		protected BaseDbEntity()
		{
			IdEntity = null;
		}

		protected BaseDbEntity(int idEntity)
		{
			IdEntity = idEntity;
		}
	}
}

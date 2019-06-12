namespace BLContracts
{
	public interface IProgramLogRegister
	{
		void SaveEvent(TypeEvent typeEvent, string location, string caption, string description);
	}
}

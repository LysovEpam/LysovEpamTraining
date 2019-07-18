using BLContracts;

namespace BL.OnlineStore.BlModels
{
	public abstract class BaseBlModel:IProgramLogRegister
	{
		private readonly IProgramLogRegister _programLogRegister;
		protected BaseBlModel(IProgramLogRegister programLogRegister)
		{
			_programLogRegister = programLogRegister;
		}

		public void SaveEvent(TypeEvent typeEvent, string location, string caption, string description)
		{
			_programLogRegister.SaveEvent(typeEvent, location, caption, description);
		}

		
	}
}

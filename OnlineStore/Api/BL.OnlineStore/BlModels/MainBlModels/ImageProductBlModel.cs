using BLContracts;
using BLContracts.MainBl;
using CommonEntities;

namespace BL.OnlineStore.BlModels.MainBlModels
{
	public class ImageProductBlModel : BaseBlModel, IImageProductBlModel
	{
		private readonly string _hostingSource;
		private readonly string _controllerPath;


		public ImageProductBlModel(IProgramLogRegister logRegister, string hostingSource, string controllerPath):base(logRegister)
		{
			_hostingSource = hostingSource;
			_controllerPath = controllerPath;
		}

		public string GetImageFullSource(string productImageSource)
		{
			return $"{_hostingSource}/{_controllerPath}/{productImageSource}";
		}

		public string GetSmallSource(string productImageSource)
		{
			throw new System.NotImplementedException();
		}
	}
}

using CommonEntities;

namespace BLContracts.MainBl
{
	public interface IImageProductBlModel
	{
		string GetImageFullSource(string productImageSource);

		string GetSmallSource(string productImageSource);
	}
}

using BLContracts.MainBl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StoreWebApi.Controllers.Main
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ImageProductController : Controller
	{
		private IImageProductBlModel _productBlModel;
		public ImageProductController(IImageProductBlModel productBlModel)
		{
			_productBlModel = productBlModel;
		}

		[HttpPost]
		[AllowAnonymous]
		public void GetImage(string imageSource)
		{

		}
	}
}

using Microsoft.IdentityModel.Tokens;

namespace StoreWebApi.AuthorizationModel
{
	public interface IJwtSigningDecodingKey
	{
		SecurityKey GetKey();
	}
}

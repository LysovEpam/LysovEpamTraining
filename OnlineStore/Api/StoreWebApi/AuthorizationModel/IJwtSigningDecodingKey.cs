using Microsoft.IdentityModel.Tokens;

namespace StoreWebApi.AuthorizationModel
{
	public interface IJwtSigningDecodingKey
	{
		// Ключ для проверки подписи (публичный)
		SecurityKey GetKey();
	}
}

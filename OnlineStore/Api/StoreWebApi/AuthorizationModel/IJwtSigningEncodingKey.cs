using Microsoft.IdentityModel.Tokens;

namespace StoreWebApi.AuthorizationModel
{
	// Ключ для создания подписи (приватный)
	public interface IJwtSigningEncodingKey
	{
		string SigningAlgorithm { get; }
		SecurityKey GetKey();
	}

	
	
}

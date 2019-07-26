using Microsoft.IdentityModel.Tokens;

namespace StoreWebApi.AuthorizationModel
{
	public interface IJwtSigningEncodingKey
	{
		string SigningAlgorithm { get; }
		SecurityKey GetKey();
	}

	
	
}

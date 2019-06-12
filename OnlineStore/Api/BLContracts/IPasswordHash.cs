namespace BLContracts
{
	public interface IPasswordHash
	{
		string GeneratePasswordHash(string login, string password);
	}
}

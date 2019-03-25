using System.IO;
using System.IO.Compression;
using System.Text;

namespace PackFile
{
	class StringCompression
	{
		private readonly Encoding _encoding;
		public StringCompression(Encoding encoding)
		{
			_encoding = encoding;
		}
		public string CompressString(string text)
		{
			byte[] byteArray = _encoding.GetBytes(text);

			using (MemoryStream stream = new MemoryStream())
			{
				using (GZipStream zipStream = new GZipStream(stream, CompressionMode.Compress))
				{
					zipStream.Write(byteArray, 0, byteArray.Length);
				}

				byteArray = stream.ToArray();
			}

			string stringResult = _encoding.GetString(byteArray);
			return stringResult;
		}

		public string DecompressString(string valueString)
		{

			byte[] byteArray = _encoding.GetBytes(valueString);

			string stringResult;


			using (GZipStream zipStream = new GZipStream(new MemoryStream(byteArray), CompressionMode.Decompress))
			{
				using (StreamReader reader = new StreamReader(zipStream, _encoding))
				{
					stringResult = reader.ReadToEnd();
				}
			}




			return stringResult;
		}

	}
}

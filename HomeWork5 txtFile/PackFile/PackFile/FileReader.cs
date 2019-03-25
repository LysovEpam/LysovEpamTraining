using System;
using System.IO;
using System.Text;

namespace PackFile
{
	class FileReader
	{

		private readonly Encoding _encoding;
		public FileReader(Encoding encoding)
		{
			_encoding = encoding;
		}

		public string GetTextFile(string filePath)
		{

			try
			{
				string result;
				using (StreamReader sr = new StreamReader(new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite), _encoding))
				{
					result = sr.ReadToEnd();
				}

				return result;
			}
			catch (Exception)
			{
				return null;
			}




		}

	}
}

using System;
using System.IO;

namespace Task1
{
	class FileReader
	{
		public string GetTextFile(string filePath)
		{

			try
			{
				string result;
				using (StreamReader sr = new StreamReader(filePath))
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

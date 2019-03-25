using System;
using System.IO;
using System.Text;

namespace PackFile
{
	class FileWriter
	{
		private readonly Encoding _encoding;
		public FileWriter(Encoding encoding)
		{
			_encoding = encoding;
		}

		public bool WriteTxtFile(string pathFile, string content)
		{
			try
			{
				using (StreamWriter sw = new StreamWriter(new FileStream(pathFile, FileMode.OpenOrCreate, FileAccess.ReadWrite), _encoding))
				{
					sw.Write(content);
				}


				return true;
			}
			catch (Exception)
			{
				return false;
			}
			
		}
	}
}

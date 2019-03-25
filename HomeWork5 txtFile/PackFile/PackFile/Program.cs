using System;
using System.Text;

namespace PackFile
{
	class Program
	{
		static void Main()
		{
			DateTime timeStart = DateTime.Now;

			Console.OutputEncoding = Encoding.Unicode;

			string pathSourceFile = @"goncharov_ivan-obyknovennaja_istorija.txt"; 
			string pathFilePack = @"pack.txt";
			string pathFileUnpack = @"unpack.txt";

			Encoding encoding = Encoding.Default;

			#region Чтение исходного файла

			FileReader fileReader = new FileReader(encoding);
			string contentSourceFile = fileReader.GetTextFile(pathSourceFile);

			#endregion
			#region Архивация и сохранение 

			//Архивация исходного текста
			StringCompression stringCompression = new StringCompression(encoding);
			string stringCompress = stringCompression.CompressString(contentSourceFile);

			//Сохранение заархивированного текста
			FileWriter fileWriter = new FileWriter(encoding);
			fileWriter.WriteTxtFile(pathFilePack, stringCompress);

			#endregion
			#region Чтение и разархивирование текста

			//Чтение из файла с архивированным текстом
			string contentPackFile = fileReader.GetTextFile(pathFilePack);

			//Распаковка текста
			string stringDecompress = stringCompression.DecompressString(contentPackFile);

			//Сохранение разорхивированного текста в новый файл
			fileWriter.WriteTxtFile(pathFileUnpack, stringDecompress);

			#endregion

			
			DateTime timeFinish = DateTime.Now;

			TimeSpan timeWork = timeFinish - timeStart;

			Console.WriteLine("Работа программы замершена!");
			Console.WriteLine($"Размер исходного текста: {contentSourceFile.Length}");
			Console.WriteLine($"Размер сжатого текста: {stringCompress.Length}");
			Console.WriteLine($"Процент сжатия текста составил: {100 - (stringCompress.Length * 100 / contentSourceFile.Length)}%");
			Console.WriteLine($"Размер распакованного текста: {stringDecompress.Length}");
			Console.WriteLine($"Время работы программы: {timeWork.Seconds}.{timeWork.Milliseconds} sec");
			Console.WriteLine($"Тест равенства исходного текста после архивации:");

			if (contentSourceFile == stringDecompress)
				Console.WriteLine("Два текста полностью совпадают!");
			else
				Console.WriteLine("Два текста отличаются!");


			Console.ReadLine();

		}


	}
}

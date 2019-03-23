using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task1
{
	class Program
	{
		static void Main()
		{
			Console.OutputEncoding = Encoding.Unicode;

			string pathFile = "Text.txt";

			FileReader readreModel = new FileReader();

			string text = readreModel.GetTextFile(pathFile);

			if (text == null)
			{
				Console.WriteLine("Text is empty");
				Console.ReadLine();
				return;
			}

			Console.WriteLine(text);
			Console.WriteLine("=======================================================================================================================");

			WordSeparator separatorModel = new WordSeparator();
			Dictionary<string, int> dictionaryResult = separatorModel.SeparateText(text);

			//Сортировака по частоте количеству вхождений слова в текст
			foreach (KeyValuePair<string, int> keyValue in dictionaryResult.OrderBy(key => key.Value).Reverse())
				Console.WriteLine($"{keyValue.Key} --- {keyValue.Value}");



			Console.ReadLine();
		}
	}
}

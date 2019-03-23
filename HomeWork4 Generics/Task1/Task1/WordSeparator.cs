using System;
using System.Collections.Generic;

namespace Task1
{
	class WordSeparator
	{
		/// <summary>
		/// Word splitting method
		/// </summary>
		/// <param name="text">Source text</param>
		/// <returns>Dictionary with information about the frequency of the word in the text</returns>
		public Dictionary<string, int> SeparateText(string text)
		{
			char[] separator = ". ,!?:;()[]{}-—\n\a\f\r\v\t".ToCharArray();

			string[] words = text.Split(separator);

			Dictionary<string, int> dictionaryResult = new Dictionary<string, int>();

			foreach (string word in words)
			{

				string wordTrim = word.Trim().ToLower();


				if (wordTrim == "")
					continue;


				if (dictionaryResult.ContainsKey(wordTrim))
					dictionaryResult[wordTrim]++;
				else
					dictionaryResult.Add(wordTrim, 1);

			}



			return dictionaryResult;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Lab4HomeWork
{
	class Program
	{
		static void Main()
		{
			Console.OutputEncoding = Encoding.Unicode;
			
			#region Проверка решения значениями из задания
			
			string[] sequenceMainTest =
			{
				//Числовые последовательности для проверки из задания:
				"123567",			//1-2-3-  -5-6-7
				"899091939495",		//89-90-91- __ -93-94-95
				"9899101102",		//98-99- __ -101-102
				"599600601602",		//599-600-601-602
				"8990919395"		//89-90-91-__-93-__-95
			};

			Console.WriteLine("Проверка решения значениями из задачи:");
			foreach (string digitalSequence in sequenceMainTest)
			{
				int result = GetMissingNumber(digitalSequence);
				Console.WriteLine($"{digitalSequence} : {result}");
			}
			#endregion
			#region Проверка решения дополнительными последовательностями
			
			string[] sequenceExtendedTest =
			{
				//Числовые последовательности для полной проверки:
				"02",				//0- __ -2
				"03",				//0- __ -3
				"13",				//1- __ -3
				"14",				//1- __ -4
				"135",				//1- __ -3- __ -5
				"57",				//5- __ -7
				"891112",			//8-9- __ -11-12
				"891012",			//8-9-10- __ -12
				"911",				//9- __ -11
				"91113",			//9- __ -11- __ -13 
				"81011",			//8- __ -10-11
				"810",				//8- __ -10
				"811",				//8- __ -11
				"8910",				//8-9-10
				"891011",			//8-9-10-11
				"8183",				//81- __ -83
				"98100",			//98- __ -100
				"99101",			//99- __ -101
				"99102",			//99- __ -102
				"99101103",			//99- __ -101- __ -103
				"98100101102",		//98- __ -100-101-102
				"9899100101102",	//98-99-100-101-102
				"98100102107104",	//98- __ -100- __ -102
				"222223225",		//222-223- __ -225
				"222224226",		//222- __ -224- __ -226
				"333334335",		//333-334-335
				"9991000",			//999-1000
				"9991001"			//999- ___ -1001
			};
			

			Console.WriteLine("\nПроверка решения дополнительными последовательностями:");
			foreach (string digitalSequence in sequenceExtendedTest)
			{
				int result = GetMissingNumber(digitalSequence);
				Console.WriteLine($"{digitalSequence} : {result}");
			}

			#endregion

			Console.ReadLine();
		}

		#region Method lookup missed number

		/// <summary>
		/// Method lookup missed number
		/// </summary>
		/// <param name="numericSequence">Numeric sequence</param>
		/// <returns>Missed number</returns>
		static int GetMissingNumber(string numericSequence)
		{
			//Массив цифр из полученной последовательности
			int[] digitArray = GetDigitArray(numericSequence);

			int digitResult = -1;

			#region Цикл перебора первого числа в последовательности

			//Размер первого числа в последовательности не может быть меньше 1 и больше половины размера всей последовательности
			for (int i = 1; i <= digitArray.Length / 2; i++)
			{
				int missingNumber = -1;         //Итоговый результат для заданной размерности первого числа
				int countMissingNumbers = 0;    //Количество пропущенных чисел

				int numberNextPosition = 0; //Позиция следующей цифры в последовательности
				int numberNextLength = i;   //Длина следующего числа в последовательности

				#region Проход всех чисел в массие

				//Проход всех чисел в массие
				while (numberNextPosition < digitArray.Length)
				{
					//Получить число в последовательности
					int firstNumber = GetNumberFromSequence(digitArray, numberNextPosition, numberNextLength);

					int secondNumber1 = firstNumber + 1;
					int secondNumber2 = firstNumber + 2;

					int secondSize1 = GetSizeNumber(secondNumber1);
					int secondSize2 = GetSizeNumber(secondNumber2);

					int secondNumberArray1 = GetNumberFromSequence(digitArray, numberNextPosition + numberNextLength, secondSize1);
					int secondNumberArray2 = GetNumberFromSequence(digitArray, numberNextPosition + numberNextLength, secondSize2);

					numberNextPosition += i;

					//Если числовая последовательность не нарушена
					if (secondNumber1 == secondNumberArray1)
					{
						numberNextLength = secondSize1;
						continue;
					}

					//Если числовая последовательность нарушена
					if (secondNumber2 == secondNumberArray2)
					{
						numberNextLength = secondSize2;
						countMissingNumbers++;
						missingNumber = secondNumber1;
						continue;
					}

					break;
				}

				#endregion

				//Если было найдено только одно пропущенное число, то сохранить его как результат и закончить поиск
				if (countMissingNumbers == 1)
				{
					digitResult = missingNumber;
					break;
				}

			}

			#endregion

			return digitResult;
		}

		#endregion
		#region Get numbers from string

		/// <summary>
		/// Get numbers from string
		/// </summary>
		/// <param name="digitalSequence">Digital sequence</param>
		/// <returns>Array of numbers</returns>
		static int[] GetDigitArray(string digitalSequence)
		{
			int[] arrayResult = new int[digitalSequence.Length];

			for (var i = 0; i < digitalSequence.Length; i++)
				arrayResult[i] = (int)char.GetNumericValue(digitalSequence[i]);

			return arrayResult;
		}

		#endregion
		#region Making a number from a sequence

		/// <summary>
		/// Method of making a number from a sequence
		/// </summary>
		/// <param name="arrayDigit">Array of numbers</param>
		/// <param name="startIndex">Starting number index</param>
		/// <param name="digitSize">Size of the final number</param>
		/// <returns>Final number result</returns>
		static int GetNumberFromSequence(int[] arrayDigit, int startIndex, int digitSize)
		{
			List<int> digitList = new List<int>();

			for (int i = 0; i < digitSize; i++)
			{
				if (i + startIndex < arrayDigit.Length)
					digitList.Add(arrayDigit[startIndex + i]);
			}

			int grad = 1;
			int numberResult = 0;

			for (int i = digitList.Count - 1; i >= 0; i--)
			{
				numberResult += grad * digitList[i];
				grad *= 10;
			}

			return numberResult;

		}

		#endregion
		#region Get the size of the number

		/// <summary>
		/// Get the size of the number
		/// </summary>
		/// <param name="number">Number</param>
		/// <returns>Size of the number</returns>
		static int GetSizeNumber(int number)
		{
			int sizeResult = 1;
			double numberTemp = number * 0.1;

			while (numberTemp >= 1)
			{
				sizeResult++;
				numberTemp *= 0.1;
			}

			return sizeResult;
		}

		#endregion
	
	}
}

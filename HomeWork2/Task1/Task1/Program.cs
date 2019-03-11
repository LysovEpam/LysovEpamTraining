using System;

namespace Task1
{
	class Program
	{
		static void Main()
		{

			int[] arrayA = { 121, 144, 19, 161, 19, 144, 19, 11 };
			int[] arrayB = { 121, 14641, 20736, 361, 25921, 361, 20736, 361 };
			bool result = CompareArrays(arrayA, arrayB);
			PrintResult(arrayA, arrayB, result);
			
			arrayA = new[] { 121, 144, 19, 161, 19, 144, 19, 11 };
			arrayB = new[] { 132, 14641, 20736, 361, 25921, 361, 20736, 361 };
			result = CompareArrays(arrayA, arrayB);
			PrintResult(arrayA, arrayB, result);
			
			arrayA = new[] { 121, 144, 19, 161, 19, 144, 19, 11 };
			arrayB = new[] { 121, 14641, 20736, 36100, 25921, 361, 20736, 361 };
			result = CompareArrays(arrayA, arrayB);
			PrintResult(arrayA, arrayB, result);
			
			arrayA = null;
			arrayB = new[] { 121, 14641, 20736, 36100, 25921, 361, 20736, 361 };
			result = CompareArrays(arrayA, arrayB);
			PrintResult(arrayA, arrayB, result);
			
			arrayA = new[] { 121, 144, 19, 161, 19, 144, 19, 11 };
			arrayB = null;
			result = CompareArrays(arrayA, arrayB);
			PrintResult(arrayA, arrayB, result);
			
			arrayA = new int[0];
			arrayB = new[] { 121, 14641, 20736, 36100, 25921, 361, 20736, 361 };
			result = CompareArrays(arrayA, arrayB);
			PrintResult(arrayA, arrayB, result);
			

			Console.ReadLine();
		}

		
		static bool CompareArrays(int[] arrayA, int[] arrayB)
		{
			if (arrayA == null || arrayB == null)
				return false;
			if (arrayA.Length == 0 || arrayB.Length == 0)
				return false;

			int[] tempArray = new int[arrayA.Length];
			for (int i = 0; i < arrayA.Length; i++)
				tempArray[i] = arrayA[i];
			

			bool result = true;

			foreach (int arrayValue in arrayB)
			{
				bool findValue = false;

				for (int i = 0; i < tempArray.Length; i++)
				{
					if (arrayValue == tempArray[i] * tempArray[i])
					{
						findValue = true;
						tempArray[i] = 0;
						break;
					}
				}

				if (!findValue)
				{
					result = false;
					break;
				}
			}


			return result;
		}

		static void PrintResult(int[] arrayA, int[] arrayB, bool result)
		{
			Console.Write("Array a: ");
			if (arrayA != null)
			{
				foreach (int arrayValue in arrayA)
					Console.Write($"{arrayValue}\t");
			}
			else
			{
				Console.Write("is null");
			}

			Console.Write("\nArray b: ");
			if (arrayB != null)
			{
				foreach (int arrayValue in arrayB)
					Console.Write($"{arrayValue}\t");
			}
			else
			{
				Console.Write("is null");
			}


			Console.WriteLine($"\nResult: {result}");
			Console.WriteLine();
		}



	}
}

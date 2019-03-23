using System;
using Task2.Model;

namespace Task2
{
	class Program
	{
		static void Main()
		{
			#region Test create list

			// Создание двух односвязных сортированных списков с разной сортировкой:

			SortedNodeList<int> sorterList1 = new SortedNodeList<int>(SortedNodeList<int>.SortedNodeListType.SortAscending)
			{
				5, -15, 10, 1, 9, 4, -5, 1, -20, 50, -11, 0, 6
			};
			SortedNodeList<int> sorterList2 = new SortedNodeList<int>(SortedNodeList<int>.SortedNodeListType.SortDescending)
			{
				5, -15, 10, 1, 9, 4, -5, 1, -20, 50, -11, 0, 6
			};

			//Вывод на экран двух списков

			Console.Write("List sort Ascending: ");
			PrintList(sorterList1);
			Console.WriteLine();
			Console.Write("List sort Descending: ");
			PrintList(sorterList2);
			Console.WriteLine();

			#endregion

			#region Add and remove test

			sorterList1.Add(-101);
			sorterList2.Add(-102);

			sorterList1.RemoveElement(10);
			sorterList2.RemoveElement(10);

			Console.Write("List one: ");
			PrintList(sorterList1);
			Console.WriteLine();
			Console.Write("List two: ");
			PrintList(sorterList2);
			Console.WriteLine();



			#endregion
			
			

			Console.WriteLine("Finish");
			Console.ReadLine();
		}


		static void PrintList<T>(SortedNodeList<T> list) where T : IComparable
		{
			foreach (var node in list)
				Console.Write(node + " ");
		}
	}


}

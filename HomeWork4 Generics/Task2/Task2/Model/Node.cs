using System;

namespace Task2.Model
{
	public class Node<T> where T:IComparable
	{
		public Node(T data)
		{
			Data = data;
		}
		public T Data { get; set; }
		public Node<T> NextNode { get; set; }
	}
}

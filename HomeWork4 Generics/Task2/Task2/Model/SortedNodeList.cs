using System;
using System.Collections;
using System.Collections.Generic;

namespace Task2.Model
{
	/// <summary>
	/// Always sorted single linked list
	/// </summary>
	/// <typeparam name="T"></typeparam>
	class SortedNodeList<T> : IEnumerable<T> where T : IComparable
	{
		/// <summary>
		/// Type sorted
		/// </summary>
		public enum SortedNodeListType
		{
			SortAscending,
			SortDescending
		}



		/// <summary>
		/// List head element
		/// </summary>
		public Node<T> Head { get; private set; }
		/// <summary>
		/// List tail element
		/// </summary>
		public Node<T> Tail { get; private set; }
		/// <summary>
		/// count list elements
		/// </summary>
		public int CountElements { get; private set; }

		/// <summary>
		/// Sort type
		/// </summary>
		public SortedNodeListType SortElementsTpe { get; }

		/// <summary>
		/// Always sorted single linked list
		/// </summary>
		/// <param name="sortType">Type sorted list</param>
		public SortedNodeList(SortedNodeListType sortType)
		{
			SortElementsTpe = sortType;
		}

		#region Add element

		/// <summary>
		/// Add new lement
		/// </summary>
		/// <param name="data">New element</param>
		public void Add(T data)
		{

			Node<T> node = new Node<T>(data);

			#region Если список пуст

			//Если список пуст, то сохранить новый элемент и покинуть метод

			if (Head == null)
			{
				Head = node;
				Tail = node;
				CountElements++;
				return;
			}

			#endregion


			Node<T> nodeOne = null;
			Node<T> nodeTwo = Head;

			while (true)
			{
				
				bool compareOne = data.CompareTo(nodeTwo.Data) <= 0;
				if (SortElementsTpe == SortedNodeListType.SortDescending)
					compareOne = !compareOne;

				//Если нужно вставить перед следующим элементов
				if (compareOne)
				{
					//Если перый элемет пустов - значит нужно вставить элемент в голову списка
					if (nodeOne == null)
					{
						node.NextNode = Head;
						Head = node;
					}
					else
					{
						//Если элемент не пустой, значит нужно вставить в середину списка
						nodeOne.NextNode = node;
						node.NextNode = nodeTwo;
					}

					break;
				}

				bool compareTwo = data.CompareTo(nodeTwo.Data) > 0;
				if (SortElementsTpe == SortedNodeListType.SortDescending)
					compareTwo = !compareTwo;
				
				//Если нужно вставить после следующего элемента
				if (compareTwo)
				{
					//Если следующий элемент пустой, значит сохраить данные и покинуть цеик
					if (nodeTwo.NextNode == null)
					{
						nodeTwo.NextNode = node;
						Tail = node;
						Tail.NextNode = null;
						break;
					}

					nodeOne = nodeTwo;
					nodeTwo = nodeTwo.NextNode;

				}




			}

			CountElements++;

		}

		#endregion
		#region Contain element in list

		/// <summary>
		/// Node list is contain element
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public bool ContainIs(T data)
		{
			Node<T> node = Head;

			while (node != null)
			{
				if (node.Data.Equals(data))
					return true;

				node = node.NextNode;
			}

			return false;
		}

		#endregion
		#region Remove element

		/// <summary>
		/// Remove node from list
		/// </summary>
		/// <param name="data">Data node</param>
		/// <returns>Node has been deleted</returns>
		public bool RemoveElement(T data)
		{
			Node<T> current = Head;
			Node<T> node = null;

			bool removeResult = false;

			while (current != null)
			{
				if (current.Data.Equals(data))
				{
					// Если элемент в самом начале списка
					if (node == null)
					{
						Head = Head.NextNode;
						//Если элемент остался один
						if (Head == null)
							Tail = null;

					}
					else
					{
						//Если элемент в конце или в середине списка
						node.NextNode = current.NextNode;
						if (current.NextNode == null)
							Tail = node;
					}

					CountElements--;
					removeResult = true;

					break;
				}
				node = current;
				current = current.NextNode;
			}


			return removeResult;
		}

		#endregion


		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this).GetEnumerator();
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			Node<T> node = Head;

			while (node != null)
			{
				yield return node.Data;

				node = node.NextNode;
			}
		}
	}
}

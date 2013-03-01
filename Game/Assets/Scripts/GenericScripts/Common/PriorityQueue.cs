using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/**
 * Homemade PriorityQueue based on a priority heap
 * More info about heaps:
 * http://en.wikipedia.org/wiki/Heap_tree#Heap_implementation
 */

namespace Pathfinding.Common {
	
	public class PriorityQueue<T> where T : IComparable<T> {
	
		private List<T> list;
		
		public PriorityQueue() {
			this.list = new List<T>();
		}
		
		public void Push(T item) {
			list.Add(item);	
			int i = list.Count -1;
			
			// recursive loop over heap
			while (i > 0) {
				int p = (i - 1) / 2; // parent
				if (list[i].CompareTo(list[p]) >= 0) break;
				T temp = list[i];
				list[i] = list[p];
				list[p] = temp;
				i = p; // recurse to parent
			}
			
		}
		
		public T Pop() {
			int lastId = list.Count - 1;
			T front = list[0];
			list[0] = list[lastId];
			list.RemoveAt(lastId);
			lastId--;
			
			int p = 0; // start from root
			// reorder the heap
			while (true) {
				int child = 2 * p + 1;
				if (child > lastId) break;
				int rightChild = child + 1;
				
				if (rightChild <= lastId && list[rightChild].CompareTo(list[child]) < 0) {
					child = rightChild; // use this child
				}
				if (list[p].CompareTo(list[child]) <= 0) {
					break; // done
				}
				T temp = list[child];
				list[child] = list[p];
				list[p] = temp;
				p = child;	
			}
			
			return front;
			
		}
		
		public T Top() {
			return list[0];
		}
		
		public int Count() {
			return list.Count;
		}
		
		public void Clear() {
			list.Clear();	
		}
	
		public override string ToString () {
			string res = "";
			for (int i = 0; i < list.Count; ++i) {
				res += list[i].ToString() + " ";
			}
			return res;
		}
		
	}
	
}



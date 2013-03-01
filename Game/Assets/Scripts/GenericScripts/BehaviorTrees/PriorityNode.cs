using System;

namespace BehaviorTrees
{
	/// <summary>
	/// A wrapper class for pairing a node with a priority.
	/// </summary>
	public class PriorityNode {
		
		public string name;
		/// <summary>
		/// Construct a priority node with prio 1.
		/// </summary>
		/// <param name='n'>
		/// N.
		/// </param>
		public PriorityNode(Node n) {
			node = n; priority = 1;
		}
		/// <summary>
		/// Construct a priority node with the given prio.
		/// </summary>
		/// <param name='n'>
		/// N.
		/// </param>
		/// <param name='prio'>
		/// Prio.
		/// </param>
		public PriorityNode(Node n, double prio) {
			node = n; priority = prio;
		}
		
		private Node node;  
		public Node Child {
			get { return node; }
		}
		private double priority;
		public double Prio {
			get { return priority; } set { priority = value; } 
		}
	}
}


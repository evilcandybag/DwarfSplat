using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace BehaviorTrees
{
	public class PrioritySelector : Selector
	{
		
		private HashSet<PriorityNode> children_;
		private Node runningNode;
		
		public PrioritySelector () : base()
		{
			children_ = new HashSet<PriorityNode>();
		}
		
		/// <summary>
		/// Visit the Selector's children in priority order. 
		/// SUCCEESS on the first succeeding child.
		/// If there is a child RUNNING, visit that Node. 
		/// </summary>
		/// <returns>
		/// SUCCESS if one child succeeds. 
		/// FAIL if all children fail. 
		/// RUNNING if there is a child that is not finished yet.
		/// </returns>
		public override Status Visit() {
			if (State == Status.RUNNING) {
				switch (runningNode.Visit()) {
				case Status.READY:
					throw new InvalidOperationException("A node should never return READY when visited!");
				//If we fail with a running node, we consider the selection to have failed, as we chose a failing option.
				case Status.FAIL:
					State = Status.READY;
					return Status.FAIL; 
				case Status.RUNNING:
					return Status.RUNNING;
				case Status.SUCCESS:
					State = Status.READY;
					return Status.SUCCESS;
				default:
					throw new InvalidOperationException("Unreachable case.");
				}
			} else {
				var children = children_.OrderByDescending(x => x.Prio);
				foreach (PriorityNode pc in children)  {
					Node child = pc.Child;
					switch (child.Visit()) {
					case Status.READY:
						throw new InvalidOperationException("A node should never return READY when visited!"); 
					case Status.RUNNING:
						State = Status.RUNNING;
						runningNode = child;
						return State;	
					case Status.SUCCESS:
						State = Status.READY;
						return Status.SUCCESS;
					}
				}
				State = Status.READY;
				return Status.FAIL;
			}
		}
		
		/// <summary>
		/// Add a new child to the Selector, creating a PriorityNode.
		/// Take care to always save the return value as that is the only way to change its priority!
		/// </summary>
		/// <param name='child'>Child.</param>
		/// <returns>A reference to the PriorityNode.</returns>
		public PriorityNode AddChild(Node child) {
			PriorityNode pn = new PriorityNode(child);
			children_.Add(pn);
			return pn;
		}
		
		/// <summary>
		/// Add a new child to the Selector, creating a PriorityNode.
		/// Take care to always save the return value as that is the only way to change its priority!
		/// </summary>
		/// <param name='child'>Child.</param>
		/// <param name="prio">The assigned priority</param>
		/// <returns>A reference to the PriorityNode.</returns>
		public PriorityNode AddChild(Node child, double prio) {
			PriorityNode pn = new PriorityNode(child,prio);
			children_.Add(pn);
			return pn;
		}
		
		/// <summary>Removes the specified child.</summary>
		/// <param name='child'>Node to remove.</param>
		/// <returns>The removed child Node.</returns>
		public Node RemoveChild(Node child) {
			PriorityNode n = children_.First(x => x.Child == child);
			children_.Remove(n);
			return n.Child;
		}
		
		/// <summary>Give the number of child nodes.</summary>
		/// <returns>The number of children.</returns>
		public int NoChildren(){
			return children_.Count;
		}
		
		/// <summary>
		/// A wrapper class for pairing a node with a priority.
		/// </summary>
		public class PriorityNode {
			public PriorityNode(Node n) {
				node = n; 
			}
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
}


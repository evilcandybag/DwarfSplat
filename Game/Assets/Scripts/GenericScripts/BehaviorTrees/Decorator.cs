using System;

namespace BehaviorTrees
{
	/// <summary>
	/// Abstract class for Decorator nodes.
	/// </summary>
	public abstract class Decorator : Node
	{
		private Node child_;
		
		public Decorator(Node child) : base() {
			child_ = child;
		}
		
		public Node Child {
			get {
				return child_;
			}
		}		
			
	}
}


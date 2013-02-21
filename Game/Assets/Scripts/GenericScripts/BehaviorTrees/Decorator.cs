using System;

namespace BehaviorTrees
{
	/// <summary>
	/// Abstract class for Decorator nodes.
	/// </summary>
	public abstract class Decorator : Node
	{
		private Node child_;
		
		/// <summary>
		/// Construct a childless decorator.
		/// </summary>
		public Decorator() : base() {
		}
		/// <summary>
		/// Construct a Decorator with the given child Node.
		/// </summary>
		/// <param name='child'>
		/// Child.
		/// </param>
		public Decorator(Node child) : base() {
			child_ = child;
		}
		
		public Node Child {
			get {
				if (child_ == null)
					throw new InvalidOperationException("Cannot access Child of childless Decorator!");
				return child_;
			}
		}
		
		public void SetChild(Node n) {
			child_ = n;
		}
			
	}
}


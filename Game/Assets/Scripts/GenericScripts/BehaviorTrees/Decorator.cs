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
		/// Sets the child Node to decorate.
		/// </summary>
		/// <value>
		/// The child Node to add.
		/// </value>
		public Node Child {
			set {
				child_ = value;
			}
		}
			
	}
}


using System;
using System.Collections.Generic;


namespace BehaviorTrees
{
	
	
	/// <summary>
	/// Abstract class for selectors. Takes care of the children.
	/// </summary>
	public abstract class Selector : Node
	{
		private List<Node> children_;
		
		protected Selector() {
			children_ = new List<Node>();
		}
		
		protected Selector(IEnumerable<Node> nodes) {
			children_ = new List<Node>();
			foreach (Node n in nodes) {
				AddChild(n);
			}
		}
		
		/// <summary>Gets the children.</summary>
		/// <value>The children.</value>
		protected List<Node> Children {
			get {
				return children_;
			}
		}
		
		/// <summary>Add a new child to the Selector.</summary>
		/// <param name='child'>Child.</param>
		public void AddChild(Node child) {
			children_.Add(child);
		}
		
		/// <summary>Removes the specified child.</summary>
		/// <param name='child'>Node to remove.</param>
		/// <returns>The removed child Node.</returns>
		public Node RemoveChild(Node child) {
			Node n = children_.Find(child);
			children_.Remove(child);
			return n;
		}
		/// <summary>Removes the child.</summary>
		/// <param name='childIx'>Child ix.</param>
		/// <returns>The removed child Node.</returns>
		public Node RemoveChild(int childIx) {
			Node n = children_.Item(childIx);
			children_.Remove(childIx);
			return n;
		}
		
		/// <summary>Give the number of child nodes.</summary>
		/// <returns>The number of children.</returns>
		public int NoChildren(){
			return children_.Count();
		}
	}
	
	
	
}


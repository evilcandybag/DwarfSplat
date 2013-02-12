using System;
using System.Collections.Generic;

namespace BehaviorTrees
{
	/// <summary>
	/// A selector that performs its subnodes in a predetermined sequence until
	/// all are fulfilled or the sequence fails.
	/// </summary>
	public class SequenceSelector : Selector
	{
		
		private List<Node> children_;
		
		public SequenceSelector() 
		{
			children_ =  new List<Node>();
		}
		
		public SequenceSelector(IEnumerable<Node> nodes) : this() {
			foreach (Node n in nodes) {
				AddChild(n);
			}
		}	
		
		/// <summary>
		/// Visit the Selector's children one by one in sequence until all succeed
		/// or one fails, thus failing the entire Selector.
		/// </summary>
		/// <returns>
		/// The status of the first READY or newly finished RUNNING child tree. 
		/// </returns>
		public override Status Visit() { //TODO: this might not be the correct behaviour. Check book!
			foreach (Node child in children_) {
				switch (child.Visit()) {
				case Status.READY:
					throw new InvalidOperationException("A node should never return READY when visited!");
				case Status.RUNNING:
					State = Status.RUNNING;
					return State;
				case Status.FAIL:
					State = Status.FAIL;
					return Status.FAIL;
				}
			}
			//We reached the end of the List with all successes. Hooray! We are
			//now ready to start again. 
			State = Status.READY;
			return Status.SUCCESS;
		}
		
		/// <summary>Add a new child to the Selector.</summary>
		/// <param name='child'>Child.</param>
		public void AddChild(Node child) {
			children_.Add(child);
		}
		/// <summary>Add a collection of children to the end of the sequence.</summary>
		/// <param name='children'>The collection to add.</param>
		public void AddChildren(IEnumerable<Node> children) {
			foreach (Node child in children) {
				children_.Add(child);
			}
		}
		
		
		/// <summary>Removes the specified child.</summary>
		/// <param name='child'>Node to remove.</param>
		/// <returns>The removed child Node.</returns>
		public Node RemoveChild(Node child) {
			Node n = children_.Find(x => x == child);
			children_.Remove(child);
			return n;
		}
		/// <summary>Removes the child.</summary>
		/// <param name='childIx'>Child ix.</param>
		/// <returns>The removed child Node.</returns>
		public Node RemoveChild(int childIx) {
			Node n = children_[childIx];
			children_.RemoveAt(childIx);
			return n;
		}
		
		/// <summary>Give the number of child nodes.</summary>
		/// <returns>The number of children.</returns>
		public int NoChildren(){
			return children_.Count;
		}
		
		
	}
}


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
		
		public SequenceSelector() : base()
		{
		}
		
		public SequenceSelector(IEnumerable<Node> nodes) : base() {
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
		public Status Visit() { //TODO: this might not be the correct behaviour. Check book!
			foreach (Node child in Children) {
				switch (child.State) {
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
		
		
	}
}


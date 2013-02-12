using System;

namespace BehaviorTrees
{
	public class PrioritySelector : Selector
	{
		private Node runningNode;
		
		public PrioritySelector () : base()
		{
		}
		
		public PrioritySelector() : base() {
			foreach (Node n in nodes) {
				AddChild(n);
			}
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
		public Status Visit() {
			if (State == Status.RUNNING) {
				return runningNode.Visit();
			} else {
				Children.Sort();
				foreach (Node child in Children)  {
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
				return Status.FAIL;
			}
		}
		
	}
}


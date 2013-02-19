using System;
using System.Collections.Generic;


namespace BehaviorTrees
{
	
	
	/// <summary>
	/// Abstract class for selectors. Takes care of the children.
	/// </summary>
	public abstract class Selector : Node
	{
		protected Node runningNode;
		
		protected Status VisitRunning() {
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
		}
	
	}
	
	
	
}


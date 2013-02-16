using System;

namespace BehaviorTrees
{
	/// <summary>
	/// Represents an Action type leaf. This is where all the cool stuff
	/// happens.
	/// </summary>
	public class Action : Leaf
	{
		private Func<Status> task_;
		
		public Action (Func<Status> action) : base()
		{
			if (action == null)
				throw new ArgumentNullException();
			task_ = action;
		}
		
		
		public override Status Visit() {
			Status s = task_();
			if (s == Status.SUCCESS) {
				State = Status.READY;
			}
			return s;
		}
		
		
	}
}


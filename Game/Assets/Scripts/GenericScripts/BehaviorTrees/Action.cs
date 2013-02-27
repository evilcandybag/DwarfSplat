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
		
		public Action () : base() {}
		
		public Action (Func<Status> action) : base()
		{
			task_ = action;
		}
		
		/// <summary>
		/// Sets the Task to perform when Visiting.
		/// </summary>
		/// <value>
		/// The task.
		/// </value>
		/// <exception cref='InvalidOperationException'>
		/// Task is single assignment.
		/// </exception>
		public Func<Status> Task {
			set {
				if (task_ != null) {
					throw new InvalidOperationException("Task already set!");
				}
				task_ = value;
			}
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


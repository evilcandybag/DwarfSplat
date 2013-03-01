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
		private Result callbackResult;
		
		
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
		
		public void ResultCallback(Result res) {
			this.callbackResult = res;
		}
		
		private Status CheckCallbackState() {
			switch (callbackResult) {
			case Result.RUNNING:
				return Status.RUNNING;
			case Result.FAIL:
				State = Node.Status.READY;
				return Status.FAIL;
			case Result.SUCCESS:
				State = Node.Status.READY;
				return Status.SUCCESS;
			default:
				throw new InvalidOperationException("UNREACHABLE!");
			}
		}
		
		public override Status Visit() {
			if (State == Status.RUNNING) {
				return CheckCallbackState();
			}
			Status s = task_();
			switch (s) {
			case Status.RUNNING:
				State = Node.Status.RUNNING;
				break;
			default: 
				State = Node.Status.READY;
				break;
			}
			return s;
		}
		
		
	}
}


using System;

namespace BehaviorTrees
{
	/// <summary>
	/// Represents an Action type leaf. This is where all the cool stuff
	/// happens.
	/// </summary>
	public class Action<T> : Leaf
	{
		// TODO: don't yet know the type of this...
		private T task_;
		
		
		public Action ()
		{
			throw new NotImplementedException(); //TODO
		}
		
		/// <summary>
		/// Sets the task.
		/// </summary>
		/// <value>
		/// The task.
		/// </value>
		/// <exception cref='ArgumentNullException'>
		/// No null values are allowed.
		/// </exception>
		public T Task {
			set {
				if (value == null) {
					throw new ArgumentNullException();
				}
				task_ = value;
			}
		}
		
		public override Status Visit() {
			return Status.FAIL;
		}
		
		
	}
}


using System;

namespace BehaviorTrees
{
	public class Condition : Leaf
	{
		private Func<bool> test_;
		
		public Condition(Func<bool> test) : base()
		{
			if (test == null) {
				throw new ArgumentNullException();
			}
			test_ = test;
		}
		
		
		/// <summary>
		/// Visit the Leaf and perform Test.
		/// <returns>if Test() then SUCCESS else FAIL</returns>
		/// </summary>
		public override Status Visit() {
			return test_() ? Status.SUCCESS : Status.FAIL;
		}
	}
}


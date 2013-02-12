using System;

namespace BehaviorTrees
{
	public class Condition : Leaf
	{
		private Func<bool> test_;
		
		/// <summary>
		/// Default constructor creates Condition that always fails.
		/// </summary>
		public Condition ()
		{
			test_ = () => false;
		}
		
		public Condition(Func<bool> test) {
			if (test == null) {
				throw new ArgumentNullException();
			}
			test_ = test;
		}
		
		/// <summary>
		/// The test to perform at this Leaf.
		/// </summary>
		/// <value>
		/// The test.
		/// </value>
		public Func<bool> Test {
			set {
				if (value == null) {
					throw new ArgumentNullException(); 
				}
				test_ = value;
			}
		}
		
		/// <summary>
		/// Visit the Leaf and perform Test.
		/// <returns>if Test() then SUCCESS else FAIL</returns>
		/// </summary>
		public Status Visit() {
			return test_() ? Status.SUCCESS : Status.FAIL;
		}
	}
}


using System;

namespace BehaviorTrees
{
	public class ConditionDecorator : Decorator
	{
		private Func<bool> condition;
		
		public ConditionDecorator (Node child, Func<bool> condition) : base(child) 
		{
			this.condition = condition;
		}
		
		public override Node.Status Visit() {
			if (condition()) {
				return Child.Visit();
			} else {
				return Node.Status.FAIL;
			}
		}
		
	}
}


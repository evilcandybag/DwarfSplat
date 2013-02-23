using System;

namespace BehaviorTrees
{
	/// <summary>
	/// Abstract decorator for handling nodes with a cooldown between each visit.
	/// </summary>
	public abstract class AbstractCooldownDecorator : Decorator
	{
		private double lastActivate, cooldown;
		
		public AbstractCooldownDecorator (double cooldown)
		{
			this.cooldown = cooldown;
		}
		
		public override Node.Status Visit() {
			double time = GetTime();
			if (time < lastActivate + cooldown) {
				return Node.Status.FAIL;
			} else {
				lastActivate = time;
				return Child.Visit();
			}
		}
		
		protected abstract double GetTime();
	}
}


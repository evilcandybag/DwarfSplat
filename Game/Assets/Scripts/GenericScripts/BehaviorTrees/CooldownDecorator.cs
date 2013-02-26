using System;

namespace BehaviorTrees
{
	/// <summary>
	/// Abstract decorator for handling nodes with a cooldown between each visit.
	/// </summary>
	public class CooldownDecorator : Decorator
	{
		private double lastActivate, cooldown;
		private Func<double> getTime;
		
		//Childless constructor.
		public CooldownDecorator (double cooldown, Func<double> getTime)
		{
			this.cooldown = cooldown;
			this.getTime = getTime;
		}
		
		/// <summary>Construct a new Cooldown decorator.</summary>
		/// <param name='cooldown'>The time of the cooldown (unit is platform dependent).</param>
		/// <param name='getTime'>Function for getting the current time in this platform.</param>
		/// <param name='child'>Child Node.</param>
		public CooldownDecorator (double cooldown, Func<double> getTime, Node child) : base (child) {
			this.cooldown = cooldown;
			this.getTime = getTime;
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
		
	}
}


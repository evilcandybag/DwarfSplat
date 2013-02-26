using System;

namespace BehaviorTrees
{
	/// <summary>
	/// Abstract decorator for handling nodes with a cooldown between each visit.
	/// </summary>
	public class CooldownDecorator : Decorator
	{
		private double lastActivate, cooldown, offset;
		private Func<double> getTime;
		
		//Childless constructor.
		public CooldownDecorator (double cooldown, Func<double> getTime) : this(cooldown,0,getTime,null){}
		public CooldownDecorator (double cooldown, double offset, Func<double> getTime) : this(cooldown,offset,getTime,null){}
		
		/// <summary>Construct a new Cooldown decorator.</summary>
		/// <param name='cooldown'>The time of the cooldown (unit is platform dependent).</param>
		/// <param name="offset">An offset between activation of the node and locking it.</param>
		/// <param name='getTime'>Function for getting the current time in this platform.</param>
		/// <param name='child'>Child Node.</param>
		public CooldownDecorator (double cooldown, double offset, Func<double> getTime, Node child) : base (child) {
			this.cooldown = cooldown;
			this.getTime = getTime;
			this.offset = offset;
		}
		
		public override Node.Status Visit() {
			double time = getTime();
			double t = lastActivate + offset;
			if (time > t && time < t + cooldown) {
				return Node.Status.FAIL;
			} else {
				lastActivate = time;
				return Child.Visit();
			}
		}
		
	}
}


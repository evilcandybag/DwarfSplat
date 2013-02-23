using System;
using UnityEngine;
using BehaviorTrees;

namespace BehaviorTrees.Unity
{
	/// <summary>
	/// Unity specific cooldown decorator.
	/// </summary>
	public class UnityCooldownDecorator : AbstractCooldownDecorator
	{
		public UnityCooldownDecorator (double cooldown) : base(cooldown)
		{
		}
		
		protected override double GetTime() {
			return Time.time;
		}
	}
}


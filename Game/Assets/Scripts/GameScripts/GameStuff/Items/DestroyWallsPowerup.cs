using UnityEngine;
using System.Collections;

public class DestroyWallsPowerup : Powerup {
	
	override
	public void powerupEffect(IActor actor) {
		Ball b = actor as Ball;
		b.startDestroyWallsPowerup();
	}
}

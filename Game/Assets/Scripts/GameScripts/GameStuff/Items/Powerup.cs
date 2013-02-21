using UnityEngine;
using System.Collections;

public abstract class Powerup : IItem {
	
	int uses;
	int maxUses;
	
	public Powerup() {
		maxUses = 1;
		uses = 1;
	}
	
	public bool hasUses() {
		if(uses > 0) {
			return true;
		}
		else {
			return false;
		}
	}
	
	public int getMaxUses() {
		return maxUses;
	}
	
	public int getUsesLeft() {
		return uses;
	}
	
	public void addUses(int amount) {
		uses = Mathf.Min(uses+amount, maxUses);
	}
	
	public bool use(IActor user){
		if(hasUses()) {
			uses --;
			powerupEffect(user);
			return true;
		}
		else {
			return false;
		}
	}
	
	public abstract void powerupEffect(IActor user);
}

using UnityEngine;
using System.Collections;

public class Mine : IItem {
	
	int uses;
	int maxUses;
	
	public Mine() {
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
			placeMine(user);
			return true;
		}
		else {
			return false;
		}
	}
	
	private void placeMine(IActor user) {
		//GroundStuffController.getGroundStuffController().createMine(user.getPosition());
	}
}

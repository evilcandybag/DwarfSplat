using UnityEngine;
using System.Collections;

public class MineActivated : IInteractable {
	
	public bool canInteract(IActor actor) {
		return true;
	}
	
	public void interact(IActor actor) {
		if (canInteract(actor)) {
			//Explode
		}
	}
}

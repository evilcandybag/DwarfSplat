using UnityEngine;
using System.Collections;

public class MineActivated : MonoBehaviour, IInteractable {
	
	public bool canInteract(IActor actor) {
		return true;
	}
	
	public void interact(IActor actor) {
		if (canInteract(actor)) {
			//Explode
		}
	}
	
	public Vector3 getPosition() {
		return this.transform.localPosition;
	}
}

using UnityEngine;
using System.Collections;
using System;

public class MineActivated : MonoBehaviour, IInteractable {
	
	public bool canInteract(IActor actor) {
		return true;
	}
	
	public void interact(IActor actor, Action<Result> callback) {
		if (canInteract(actor)) {
			//Explode
		}
	}
	
	public Vector3 getPosition() {
		return this.transform.localPosition;
	}
}

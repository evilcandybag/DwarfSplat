using UnityEngine;
using System.Collections;
using System;

public class Workspace : MonoBehaviour, IInteractable {

	public void interact(IActor dwarf, Action<Result> callback) {
		if (canInteract(dwarf)) {
			//make dwarf enter work mode
		}
	}
	
	/**
	 * only checks if the type of actor is correct, distance etc. must be checked elsewhere
	 * */
	public bool canInteract(IActor actor) {
		if (actor is Dwarf) {
			return true;
		}
		else {
			return false;
		}
	}
	
	public Vector3 getPosition() {
		return this.transform.localPosition;
	}
}

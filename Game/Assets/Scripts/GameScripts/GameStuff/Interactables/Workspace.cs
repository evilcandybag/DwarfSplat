using UnityEngine;
using System.Collections;

public class Workspace : IInteractable {

	public void interact(IActor dwarf) {
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
}

using UnityEngine;
using System.Collections;
using System;

public class Bed : MonoBehaviour, IInteractable {
	
	public void interact(IActor dwarf, Action<Result> callback) {
		if (canInteract(dwarf) && dwarf is Dwarf) {
			Dwarf d = dwarf as Dwarf;
			//make dwarf enter sleep mode
			Debug.Log("Dwarf now sleeping <3");
			d.State = Dwarf.Status.SLEEP;
			callback(Result.RUNNING);
			
		}
		else {
			callback(Result.FAIL);
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
		return this.transform.position;
	}
}

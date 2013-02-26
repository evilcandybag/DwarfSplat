using UnityEngine;
using System.Collections;
using System;

public class ItemOnGround : MonoBehaviour, IInteractable {
	IItem item;
	
	public ItemOnGround(IItem item, Vector3 position) {
		this.item = item;
	}
	
	public bool canInteract(IActor actor) {
		return true;
	}
	
	public void interact(IActor actor, Action<Result> callback) {
		if (canInteract(actor)) {
			ICommand command = new PickUpCommand(actor, item);
			command.execute();
		}
	}
	
	public Vector3 getPosition() {
		return this.transform.localPosition;
	}
	
}

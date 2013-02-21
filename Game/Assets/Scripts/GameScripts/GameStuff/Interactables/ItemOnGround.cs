using UnityEngine;
using System.Collections;

public class ItemOnGround : IInteractable {
	IItem item;
	
	public ItemOnGround(IItem item, Vector3 position) {
		this.item = item;
	}
	
	public bool canInteract(IActor actor) {
		return true;
	}
	
	public void interact(IActor actor) {
		if (canInteract(actor)) {
			ICommand command = new PickUpCommand(actor, item);
			command.execute();
		}
	}
	
}

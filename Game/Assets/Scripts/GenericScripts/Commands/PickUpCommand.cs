using UnityEngine;
using System.Collections;

public class PickUpCommand : ICommand {

	IActor actor;
	IItem item;
	
	public PickUpCommand(IActor a, IItem i) {
		actor = a;
		item = i;
	}
	
	public bool isAllowed() {
		return true;
	}
	
	public void execute() {
		if(isAllowed())
			actor.addItem(item);
	}
}

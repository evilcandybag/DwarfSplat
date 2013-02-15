using UnityEngine;
using System.Collections;

public class PickUpCommand : ICommand {

	IActor actor;
	IItem item;
	
	public PickUpCommand(IActor a, IItem i) {
		actor = a;
		item = i;
	}
	
	bool ICommand.isAllowed() {
		return false;
	}
	
	void ICommand.execute() {
		
	}
}

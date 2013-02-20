using UnityEngine;
using System.Collections;

public class InteractCommand : ICommand {
	
	IActor actor;
	IInteractable interactable;
	
	public InteractCommand(IActor actor, IInteractable interactable) {
		this.actor = actor;
		this.interactable = interactable;
		
	}

	public bool isAllowed() {
		//TODO lots of stuffz to check if the movement can be allowed
		return false;
	}
	
	public void execute() {
		
	}
}
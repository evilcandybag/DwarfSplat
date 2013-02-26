using UnityEngine;
using System.Collections;
using System;

public class InteractCommand : ICommand {
	
	IActor actor;
	IInteractable interactable;
	Action<Result> callback;
	
	public InteractCommand(IActor actor, IInteractable interactable, Action<Result> callback) {
		this.actor = actor;
		this.interactable = interactable;
		this.callback = callback;
	}

	public bool isAllowed() {
		//TODO lots of stuffz to check if the movement can be allowed
		return false;
	}
	
	public void execute() {
		
	}
}
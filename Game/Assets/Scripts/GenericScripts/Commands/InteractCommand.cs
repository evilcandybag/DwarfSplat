using UnityEngine;
using System.Collections;
using System;

public class InteractCommand : ICommand {
	
	IActor actor;
	IInteractable interactable;
	Action<Result> callback;
	
	public static float minimalDistance = 0.5f;
	
	public InteractCommand(IActor actor, IInteractable interactable, Action<Result> callback) {
		this.actor = actor;
		this.interactable = interactable;
		this.callback = callback;
	}

	public bool isAllowed() {
		//TODO lots of stuffz to check if the interaction can be allowed
		if (interactable != null && actor != null) {
			if (Vector3.Distance(actor.getPosition(), interactable.getPosition()) < minimalDistance)
				return true;
		}
		return false;
	}
	
	public void execute() {
		if (isAllowed()) {
			interactable.interact(actor);
		}
	}
}
using UnityEngine;
using System.Collections;
using System;

public interface IInteractable {
	void interact(IActor actor, Action<Result> callback);
	
	bool canInteract(IActor actor);
	
	Vector3 getPosition();
}

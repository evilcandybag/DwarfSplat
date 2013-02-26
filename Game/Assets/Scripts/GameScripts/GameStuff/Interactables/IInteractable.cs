using UnityEngine;
using System.Collections;

public interface IInteractable {
	void interact(IActor actor);
	
	bool canInteract(IActor actor);
	
	Vector3 getPosition();
}

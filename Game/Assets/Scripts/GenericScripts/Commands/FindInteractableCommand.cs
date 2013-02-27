using UnityEngine;
using System.Collections;
using System;

public class FindInteractableCommand : ICommand {
	
	IInteractable interactable;
	Action<IInteractable> foundInteractable;
	
	public FindInteractableCommand(InteractableController.InteractableType type, Action<IInteractable> action) {
		
		this.foundInteractable = action;
		int count;
		switch (type) {
		case InteractableController.InteractableType.BED:
			count = InteractableController.Instance.getAllBeds().Count;
			
			if (count > 0) {
				int nr = (int) (UnityEngine.Random.value * count);
				interactable = InteractableController.getInteractableController().getAllBeds()[nr];
			}
			break;
		case InteractableController.InteractableType.WORKSPACE:
			count = InteractableController.Instance.getAllWorkspaces().Count;
			
			if (count > 0){
				int nr = (int) (UnityEngine.Random.value * count);;
				interactable = InteractableController.Instance.getAllWorkspaces()[nr];
			}
			break;
		default:
			interactable = null;
			break;
		}
	}
	
	public bool isAllowed() {
		if (interactable != null)
			return true;
		else
			return false;
	}
	
	/**
	 * finds a interactable of the chosen type, not necessarily the closest one
	 * */
	public void execute () {
		if (isAllowed()) {
			foundInteractable(interactable);
		}
		//find the thing you wantz
	}
}

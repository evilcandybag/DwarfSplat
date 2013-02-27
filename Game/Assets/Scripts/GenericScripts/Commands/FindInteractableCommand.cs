using UnityEngine;
using System.Collections;
using System;

public class FindInteractableCommand : ICommand {
	
	IInteractable interactable;
	Action<IInteractable> foundInteractable;
	
	public FindInteractableCommand(InteractableController.InteractableType type, Action<IInteractable> action) {
		
		this.foundInteractable = action;
		
		if (type.Equals(InteractableController.InteractableType.BED)) {
			
			int count = InteractableController.getInteractableController().getAllBeds().Count;
			
			if (count > 1) {
				int nr = (new System.Random(count)).Next();
				interactable = InteractableController.getInteractableController().getAllBeds()[nr];;
			}
		}
		else if(type.Equals(InteractableController.InteractableType.WORKSPACE)) {
			int count = InteractableController.getInteractableController().getAllWorkspaces().Count;
			
			if (count > 1){
				int nr = (new System.Random(count)).Next();
				interactable = InteractableController.getInteractableController().getAllWorkspaces()[nr];
			}
		}
		else
			interactable = null;
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

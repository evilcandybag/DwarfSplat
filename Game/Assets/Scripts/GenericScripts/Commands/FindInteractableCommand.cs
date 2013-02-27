using UnityEngine;
using System.Collections;
using System;

public class FindInteractableCommand : ICommand {
	
	IInteractable interactable;
	IActor actor;
	Action<IInteractable> foundInteractable;
	
	public FindInteractableCommand(IActor actor, InteractableController.InteractableType type, Action<IInteractable> action) {
		this.actor = actor;
		this.foundInteractable = action;
		
		if (type.Equals(InteractableController.InteractableType.BED)) {
			
			int count = InteractableController.getInteractableController().getAllBeds().Count;
			
			if (count > 1) {
				int nr = (new System.Random(count)).Next();
				
				Bed[] beds = InteractableController.getInteractableController().getAllBeds().ToArray();
				
				interactable = beds[nr];
				
			}
		}
		else if(type.Equals(InteractableController.InteractableType.WORKSPACE)) {
			int count = InteractableController.getInteractableController().getAllWorkspaces().Count;
			
			if (count > 1){
			
				int nr = (new System.Random(count)).Next();
				
				Workspace[] workspaces = InteractableController.getInteractableController().getAllWorkspaces().ToArray();
				
				interactable = workspaces[nr];
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

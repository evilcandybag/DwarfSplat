using UnityEngine;
using System.Collections;

public class FindInteractableCommand : ICommand {
	
	IInteractable interactable;
	IActor actor;
	
	public FindInteractableCommand() {
		
	}
	
	public bool isAllowed() {
		
		return true;
		
	}
	
	public void execute () {
		
		//find the thing you wantz
	}
	
	

	
}

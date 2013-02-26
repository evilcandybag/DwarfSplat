using UnityEngine;
using System.Collections.Generic;

public class InteractableController : MonoBehaviour {
	
	private static InteractableController ic;

	public List<IInteractable> allInteractables;
	public List<Bed> allBeds;
	public List<Workspace> allWorkspaces;
	
	private InteractableController() {
		allInteractables = new List<IInteractable>();
		allBeds = new List<Bed>();
		allWorkspaces = new List<Workspace>();
	}
	
	public static InteractableController getInteractableController() {
		if (ic==null)
			ic = new InteractableController();
		return ic;
	}
	
	public void destoyInteractable(IInteractable i) {
		allInteractables.Remove(i);
		
		if (i is Bed) {
			allBeds.Remove((Bed) i);
			Destroy((Bed) i);
		}
		else if (i is Workspace) {
			allWorkspaces.Remove((Workspace) i);
			Destroy ((Workspace) i);
		}
		
	}
	
	
	public void createBed(Vector3 position) {
	
	}
	
	public void createWorkspace(Vector3 position) {
	
	}
	
	public List<IInteractable> getAllInteractables() {
		return allInteractables;
	}
	public List<Bed> getAllBeds() {
		return allBeds;
	}
	public List<Workspace> getAllWorkspaces() {
		return allWorkspaces;
	}
}

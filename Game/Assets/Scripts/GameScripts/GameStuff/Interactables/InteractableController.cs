using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class InteractableController {
	
	
	public enum InteractableType {BED, WORKSPACE};
	
	private static InteractableController ic;

	public List<IInteractable> allInteractables {
		get { throw new System.NotImplementedException();}
	}
	
	public List<Bed> allBeds;
	public List<Workspace> allWorkspaces;
	
	private InteractableController() {
		allBeds = new List<Bed>();
		allWorkspaces = new List<Workspace>();
	}
	
	public static InteractableController getInteractableController() {
		if (ic==null)
			ic = new InteractableController();
		return ic;
	}
	
	public static InteractableController Instance {
		get { return InteractableController.getInteractableController(); }
	}
	
	public void destoyInteractable(IInteractable i) {
		allInteractables.Remove(i);
		
		if (i is Bed) {
			allBeds.Remove((Bed) i);
			Object.Destroy((Bed) i);
		}
		else if (i is Workspace) {
			allWorkspaces.Remove((Workspace) i);
			Object.Destroy ((Workspace) i);
		}
		
	}
	
	
	public void addBed(Bed bed) {
		allBeds.Add(bed);
	}
	
	public void addWorkspace(Workspace work) {
		allWorkspaces.Add(work);
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

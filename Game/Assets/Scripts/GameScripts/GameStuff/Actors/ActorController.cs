using UnityEngine;
using System.Collections.Generic;

public class ActorController : MonoBehaviour {
	
	private static ActorController ac;
		
	private List<Ball> ballActors;
	private List<Dwarf> dwarfActors;
	private List<IActor> allActors;
		
	private ActorController() {
		ballActors = new List<Ball>();
		dwarfActors = new List<Dwarf>();
		allActors = new List<IActor>();
	}
	
	public static ActorController getActorController() {
		if (ac==null)
			ac = new ActorController();
		return ac;
	}
	
	public void createBall(Vector3 position) {
	
	}
	
	public void createDwarf(Vector3 position) {
		/*Dwarf dwarf = new Dwarf();
		Instantiate(dwarf);
		dwarf.transform.position = position;*/
	}
	
	public void destoyActor(IActor a) {
		allActors.Remove(a);
		
		if (a is Dwarf) {
			dwarfActors.Remove((Dwarf) a);
			Destroy((Dwarf) a);
		}
		else if (a is Ball) {
			ballActors.Remove((Ball) a);
			Destroy ((Ball) a);
		}
		
	}
	
	public List<Ball> getBallActors() {
		return ballActors;
	}
	public List<Dwarf> getDwarfActors() {
		return dwarfActors;
	}
	public List<IActor> getAllActors() {
		return allActors;
	}
}

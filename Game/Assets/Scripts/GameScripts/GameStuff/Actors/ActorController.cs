using UnityEngine;
using System.Collections.Generic;

public class ActorController {
	
	private static ActorController ac;
		
	private List<IActor> ballActors;
	private List<IActor> dwarfActors;
	private List<IActor> allActors;
		
	private ActorController() {
		ballActors = new List<IActor>();
		dwarfActors = new List<IActor>();
		allActors = new List<IActor>();
	}
	
	public static ActorController getActorController() {
		if (ac==null)
			ac = new ActorController();
		return ac;
	}
	
	public void addBall(Ball b) {
		ballActors.Add(b);
	}
	
	public void addDwarf(Dwarf d) {
		dwarfActors.Add(d);
	}
	
	public void destoyActor(IActor a) {
		allActors.Remove(a);
		
		if (a is Dwarf) {
			Dwarf d = a as Dwarf;
			dwarfActors.Remove(d);
			d.Manager.Decommission(d);
			Object.Destroy(d.gameObject);
		}
		else if (a is Ball) {
			Ball b = a as Ball;
			ballActors.Remove(b);
			Object.Destroy (b.gameObject);
		}
		
	}
	
	public List<IActor> getBallActors() {
		return ballActors;
	}
	public List<IActor> getDwarfActors() {
		return dwarfActors;
	}
	public List<IActor> getAllActors() {
		return allActors;
	}
}

using UnityEngine;
using System.Collections;

public class MoveCommand : ICommand {
	
	IActor actor;
	Vector3 location;
	int speed;
	
	public MoveCommand(IActor actor, Vector3 location, int speed) {
		this.actor = actor;
		this.location = location;
		this.speed = speed;
		
	}

	public bool isAllowed() {
		//TODO can location be reached? 
		return false;
	}
	
	public void execute() {
		//TODO make a path and start the dwarf's movement (jeremy fixez?)
	}
}

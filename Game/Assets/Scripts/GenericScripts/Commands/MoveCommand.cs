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
		//TODO lots of stuffz to check if the movement can be allowed
		return false;
	}
	
	public void execute() {
		
	}
}

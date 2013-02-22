using UnityEngine;
using System.Collections;

public class MoveCommand : ICommand {
	
	IActor actor;
	Vector3 location;
	
	int speed; //1 => slow, 2 => normal, 3 => run
	
	public MoveCommand(IActor actor, Vector3 location, int speed) {
		this.actor = actor;
		this.location = location;
		
		if(speed < 1)
			this.speed = 1;
		else if(speed > 3)
			this.speed = 3;
		else
			this.speed = speed;
		
	}

	public bool isAllowed() {
		//TODO can location be reached? 
		return true;
	}
	
	public void execute() {
		if(isAllowed()) {
			actor.getComponent<MovementAgent>().MoveTo(location, speed*100);
		}
	}
}

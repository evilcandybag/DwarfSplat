using UnityEngine;
using System.Collections;
using System;

public class MoveCommand : ICommand {
	
	AbstractAIActor actor;
	Vector3 location;
	Action<Result> callback;
	
	int speed; //1 => slow, 2 => normal, 3 => run
	
	public MoveCommand(AbstractAIActor actor, Vector3 location, int speed, Action<Result> callback) {
		this.actor = actor;
		this.location = location;
		
		if(speed < 1)
			this.speed = 1;
		else if(speed > 3)
			this.speed = 3;
		else
			this.speed = speed;
		
		this.callback = callback;
		
	}

	public bool isAllowed() {
		//TODO can location be reached? can the dwarf move? Atm: yes!
		return true;
	}
	
	public void execute() {
		if(isAllowed()) {
			Debug.Log("actor is "+ (actor == null));
			Debug.Log("getcomponent is "+ (actor.GetComponent<MovementAgent>() == null));
			actor.GetComponent<MovementAgent>().MoveTo(location, speed*100, callback);
		}
	}
}

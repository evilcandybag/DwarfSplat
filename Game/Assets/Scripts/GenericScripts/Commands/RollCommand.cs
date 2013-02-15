using UnityEngine;
using System.Collections;

public class RollCommand : ICommand {

	IActor actor;
	Vector3 direction;
	int force;
	
	public RollCommand(IActor a, Vector3 direction, int force) {
		actor = a;
		this.direction = direction;
		this.force = force;
	}
	
	bool ICommand.isAllowed() {
		return false;
	}
	
	void ICommand.execute() {
		
	}
}

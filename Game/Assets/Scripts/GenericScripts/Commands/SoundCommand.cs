using UnityEngine;
using System.Collections;

public class SoundCommand : ICommand {
	
	public SoundCommand(Vector3 location, string sound) {
		
	}
	
	public bool isAllowed() {
		return true;
	}
	
	public void execute() {
		//playsound
	}
}

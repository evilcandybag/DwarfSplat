using UnityEngine;
using System.Collections;

public class EmoteCommand : ICommand {
	
	public EmoteCommand(IActor actor, string emote) {
		
	}
	
	public bool isAllowed() {
		return true;
	}
	
	public void execute() {
		//cancel old emote if there is one
		//display new emote
	}
}

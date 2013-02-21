using UnityEngine;
using System.Collections;

public class EmoteCommand : ICommand {
	
	public EmoteCommand(IActor actor, string emote) {
		
	}
	
	public bool isAllowed() {
		return true;
	}
	
	public void execute() {
		//display emote
	}
}

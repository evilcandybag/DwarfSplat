using UnityEngine;
using System.Collections;

public class UseItemCommand : ICommand {
	IActor actor;
	IItem item;
	
	public UseItemCommand(IActor a, IItem i) {
		actor = a;
		item = i;
	}
	
	public bool isAllowed() {
		return false;
	}
	
	public void execute() {
		
	}
}
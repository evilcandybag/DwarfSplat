using UnityEngine;
using System.Collections;

public interface ICommand {
	
	bool isAllowed();
	
	void execute();
	
}

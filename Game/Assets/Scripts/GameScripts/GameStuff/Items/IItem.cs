using UnityEngine;
using System.Collections;

public interface IItem  {
	
	bool hasUses();
	int getMaxUses();
	int getUsesLeft();
	void addUses(int amount);
	bool use(IActor user);
	
}

using System;
using UnityEngine;


public abstract class AbstractAIActor : MonoBehaviour, IAgent, IActor
{
	public abstract void RunAI();
	private IItem inventory;
	
	public IItem[] getInventory() {
		
		return new IItem[]{inventory};
	}
	
	public bool useItem(IItem item) {
		if (hasItem(item)) {
			item.use(this);
			return true;
		}
		else
			return false;
	}
	
	public bool addItem(IItem item) {
		inventory = item;
		return true;
		
	}
	
	public bool hasItem(IItem item) {
		if (item.Equals(inventory)) {
			return true;
		}
		else {	
			return false;
		}
	}
}


using UnityEngine;
using System.Collections;

public interface IActor {
	
	IItem[] getInventory();
	
	bool addItem(IItem item);
	
	bool useItem(IItem item);
}

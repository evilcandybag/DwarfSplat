using UnityEngine;
using System.Collections;

public class Dwarf : MonoBehaviour, IActor {
	
	IItem inventory;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
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

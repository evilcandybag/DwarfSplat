using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour, IActor {
	
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
		if (item.hasUses()) {
			
			return item.use(this);
		}
		else
			return false;
	}
	
	public bool addItem(IItem item) {
		inventory = item;
		return true;
	}
}

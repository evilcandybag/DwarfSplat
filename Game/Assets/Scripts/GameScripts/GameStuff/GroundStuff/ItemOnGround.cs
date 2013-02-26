using UnityEngine;
using System.Collections.Generic;
using System;

public class ItemOnGround : MonoBehaviour {
	IItem item;
	
	private static float pickupRadius = 0.2f;
	
	List<IActor> list;
	
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//TODO not check every frame
		foreach(IActor a in list){
			if (Vector3.Distance(a.getPosition(), getPosition()) < pickupRadius) {
				ICommand command = new PickUpCommand(a, item);
				command.execute();
				Destroy(this);
				//comment be here
				break;
			}
		}
	}
	
	public ItemOnGround(IItem item, Vector3 position, List<IActor> actorsThatCanPick) {
		//TODO instantiate and place item on position
		this.item = item;
		this.list = actorsThatCanPick;
	}
	
	public Vector3 getPosition() {
		return this.transform.localPosition;
	}
	
}

using UnityEngine;
using System.Collections.Generic;
using System;

public class MineActivated : MonoBehaviour {
	
	private static float blowupRadius = 0.2f;
	
	private List<IActor> list;
	
	public MineActivated(Vector3 position, List<IActor> actorsThatCanBlow) {
		//TODO setposition
		list = actorsThatCanBlow;
	}
	
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//TODO not check every frame
		foreach(IActor a in list){
			if (Vector3.Distance(a.getPosition(), getPosition()) < blowupRadius) {
				//TODO somehow blow up the mine
				Destroy(this);
				break;
			}
		}
	}
	
	public Vector3 getPosition() {
		return this.transform.localPosition;
	}
}

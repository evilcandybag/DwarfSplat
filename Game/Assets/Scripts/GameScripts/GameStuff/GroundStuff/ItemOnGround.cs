using UnityEngine;
using System.Collections.Generic;
using System;

public class ItemOnGround : MonoBehaviour {
	IItem item;
	
	public List<IActor> list;
	
	void Start () {
		
	}
	
	void OnCollisionEnter(Collision collision) {
		GameObject go = collision.contacts[0].otherCollider.gameObject;
		Ball ball = go.GetComponent<Ball>();
		if(collision.contacts[0].otherCollider.name.Equals("Ball(Clone)")) {
			ICommand command = new PickUpCommand(ball, item);
			command.execute();
			GroundStuffController.getGroundStuffController().destroyItem(this);
		}
	}
	
	public void setProperties(List<IActor> actors, IItem item) {
		this.list = actors;
		this.item = item;
	}
	
	public Vector3 getPosition() {
		return this.transform.localPosition;
	}
	
}

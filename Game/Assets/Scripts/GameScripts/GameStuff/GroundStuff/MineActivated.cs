using UnityEngine;
using System.Collections.Generic;
using System;

public class MineActivated : MonoBehaviour {
	
	private List<IActor> list;
	
	public void setProperties(List<IActor> actors) {
		this.list = actors;
	}
	
	void Start () {
		
	}
	
	void OnCollisionEnter(Collision collision) {
		GameObject go = collision.contacts[0].otherCollider.gameObject;
		Ball ball = go.GetComponent<Ball>();
		if(list.Contains(ball) && collision.contacts[0].otherCollider.name.Equals("Ball(Clone)")) {
			go = GameObject.Find("emptyCreationStuff");
			go.GetComponent<ExplotionScript>().Explode(this.gameObject.transform.position);
			explode(ball);
			GroundStuffController.getGroundStuffController().destoyMine(this);
			
		}
	}
	
	public Vector3 getPosition() {
		return this.transform.localPosition;
	}
	
	private void explode(Ball ball) {
		ball.transform.rigidbody.AddForce(0,1500,0);
	}
}

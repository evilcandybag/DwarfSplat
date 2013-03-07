using UnityEngine;
using System.Collections;

public class DwarfSplatter : MonoBehaviour {
	private int score;
	
	void Awake() {
		score = 0;
	}
	// Use this for initialization
	void OnCollisionEnter(Collision collision) {
		// check if this is a dwarf. 
		GameObject col = collision.contacts[0].otherCollider.gameObject;
		Dwarf d = col.GetComponent<Dwarf>();
		if (d != null) {
			ActorController.getActorController().destoyActor(d);
			score++;
		}
	}
	
	void OnGui() {
		GUI.color = Color.white;
		GUI.Box(new Rect(90,90,350,175), "");
		GUI.Label(new Rect(100,100,500,500), "Score: " + score);
	}
}

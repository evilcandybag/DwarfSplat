using UnityEngine;
using System.Collections;

public class BallCollisionScript : MonoBehaviour {
	
	bool isVisible = false;
	
	void OnCollisionEnter(Collision collision) {
		
		//Debug.Log("This collider collided with: " + collision.contacts[0].otherCollider.name);
		
		// Change name the objects name depending on what we want the wall to react with
		if(collision.contacts[0].otherCollider.name.Equals("PowerUp(Clone)")) {
			isVisible = true;
			
			Destroy(collision.contacts[0].otherCollider.gameObject);	

			GameObject go = GameObject.Find("emptyMapStuff");
			
		}
    }
	
	void OnGUI(){
		if(isVisible){
			GUI.color = Color.black;
			GUI.Label(new Rect(50,50,50,50), "DESTRUCTIBLE WALLS MODE!!!!");
		}
	}
}

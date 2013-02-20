using UnityEngine;
using System.Collections;

/// <summary>
/// Handles the ball collision to the wall
/// Julia Adamsson 2013
/// </summary>

public class WallCollisionScript : MonoBehaviour {

	void OnCollisionEnter(Collision collision) {
		
		//Debug.Log("This collider collided with: " + collision.contacts[0].otherCollider.name);
		
		// Change name the objects name depending on what we want the wall to react with
		if(collision.contacts[0].otherCollider.name.Equals("Cube")) {

			Vector3 contactPoint = collision.contacts[0].point;
			GameObject go = GameObject.Find("ScriptComponent");
			WallMeshManagerScript meshManager = (WallMeshManagerScript) go.GetComponent<WallMeshManagerScript>();			
			meshManager.CreateCrushedWallWrapper(this.gameObject);
			Destroy(this.gameObject);
			
		}
    }
}

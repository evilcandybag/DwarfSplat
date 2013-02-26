using UnityEngine;
using System.Collections;

/// <summary>
/// Handles the ball collision to the wall
/// Julia Adamsson 2013
/// </summary>

public class WallCollisionScript : MonoBehaviour {
	
	void OnCollisionEnter(Collision collision) {
		
		Debug.Log("This collider collided with: " + collision.contacts[0].otherCollider.name);
		
		// Change name the objects name depending on what we want the wall to react with
		if(collision.contacts[0].otherCollider.name.Equals("Cube")) {
					
			Vector3 contactPoint = collision.contacts[0].point;
			/*GameObject go = GameObject.Find("emptyMapStuff");
			WallMeshManagerScript meshManager = (WallMeshManagerScript) go.GetComponent<WallMeshManagerScript>();			
			meshManager.CreateCrushedWallWrapper(this.gameObject);
			Destroy(this.gameObject);*/
			
			SubdivideMeshScript sms = GetComponent<SubdivideMeshScript>();
			
			Vector3 a = contactPoint;
			
			sms.MySubdivide(false);
			
			GameObject go = GameObject.Find("emptyMapStuff");
			WallMeshManagerScript meshManager = (WallMeshManagerScript) go.GetComponent<WallMeshManagerScript>();			
			//meshManager.CreateCrushedWallWrapper(this.gameObject);
			//Destroy(this.gameObject);
			meshManager.MyCrush(this.gameObject);
			Destroy(this.gameObject);
		}
    }	
}


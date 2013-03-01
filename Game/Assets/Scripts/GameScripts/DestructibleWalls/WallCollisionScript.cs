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
			GameObject go = GameObject.Find("emptyMapStuff");
			WallMeshManagerScript meshManager = (WallMeshManagerScript) go.GetComponent<WallMeshManagerScript>();
			SubdivideMeshScript sms = GetComponent<SubdivideMeshScript>();
			
			// Removes the wall from the list and get p0 and p2 to send to jeremys method.
			meshManager.RemovedWallPos(this.gameObject);
			
			// Subdivide the front side of the wall to 8 from 2 triangles
			sms.MySubdivide(false);
			
			// Crush this wall
			meshManager.CrushWallWrapper(this.gameObject);			
			
			Destroy(this.gameObject);
		}
    }	
}


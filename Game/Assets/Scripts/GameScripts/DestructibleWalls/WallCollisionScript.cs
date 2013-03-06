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
		if(collision.contacts[0].otherCollider.name.Equals("Ball(Clone)")) {
			Vector3 contactPoint = collision.contacts[0].point;
			GameObject go = GameObject.Find("emptyCreationStuff");
			GameObject camera = GameObject.Find ("Main Camera");
			if (camera == null) {
				Debug.Log("Camera object not found!");
			}
			camera.GetComponent<CameraScript>().Shake();
			WallMeshManagerScript meshManager = (WallMeshManagerScript) go.GetComponent<WallMeshManagerScript>();
			SubdivideMeshScript sms = GetComponent<SubdivideMeshScript>();
			
			// Removes the wall from the list and get p0 and p2 to send to jeremys method.
			Vector3[] theVerts = meshManager.RemovedWallPos(this.gameObject);
			
			// Subdivide the front side of the wall to 8 from 2 triangles
			sms.MySubdivide(false);
			
			// Crush this wall
			meshManager.CrushWallWrapper(this.gameObject);			
			
			// EXPLOTION OMFG!!!! ITS SO COOL - SHIT YAH!
			go.GetComponent<ExplotionScript>().Explode(this.gameObject.transform.position);
			
			this.gameObject.layer = LayerMask.NameToLayer("Default");
			Destroy(this.gameObject);
			
			// Call Jeremys method here!! or something maybe in wallcollision. well see
			go.GetComponent<TileGraphGenerator>().Rescan(theVerts[0], theVerts[1]);
		}
    }	
}


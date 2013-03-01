using UnityEngine;
using System.Collections;


/// <summary>
/// Ball collision script.
/// Julia Adamsson 2013
/// </summary>
public class BallCollisionScript : MonoBehaviour {
	
	bool isVisible = false;
	ArrayList allWalls;
	
	public GUIStyle myStyle;
	
	void OnCollisionEnter(Collision collision) {
		
		//Debug.Log("This collider collided with: " + collision.contacts[0].otherCollider.name);
		
		// Change name the objects name depending on what we want the wall to react with
		if(collision.contacts[0].otherCollider.name.Equals("PowerUp(Clone)")) {
			isVisible = true;
			
			print("POWER UP YEAHHEHEHEHEHMGMGMHGJHDGLA OMFG!!! EHHEHE");
			
			Destroy(collision.contacts[0].otherCollider.gameObject);	

			GameObject go = GameObject.Find("emptyCreationStuff");
			allWalls = go.GetComponent<WallMeshManagerScript>().wallList;
			
			foreach(GameObject g in allWalls){
				if(g.GetComponent<SubdivideMeshScript>() == null && g.GetComponent<WallCollisionScript>() == null){
					g.AddComponent("SubdivideMeshScript");
					g.AddComponent("WallCollisionScript");
				}
			}
			
			StartCoroutine(Wait(10.0f));
		}
    }
	
	
	private IEnumerator Wait(float seconds) {
		
        yield return new WaitForSeconds(seconds);
		
		foreach(GameObject g in allWalls){
			Destroy(g.GetComponent<SubdivideMeshScript>());
			Destroy(g.GetComponent<WallCollisionScript>());
		}
		isVisible = false;
	
	}
	
	void OnGUI(){
		if(isVisible){			
			GUI.Label(new Rect(Screen.width/2,50,50,500), "!!Destructible Walls mode!!", myStyle);
		}
	}
}
				
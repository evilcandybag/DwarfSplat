using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour, IActor {
	
	IItem inventory;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public IItem[] getInventory() {
		
		return new IItem[]{inventory};
	}
	
	public bool useItem(IItem item) {
		if (item.hasUses()) {
			
			return item.use(this);
		}
		else
			return false;
	}
	
	public bool addItem(IItem item) {
		inventory = item;
		return true;
	}
	
	public void addInteractable(IInteractable i) {
		//do nothing, ball does not use interactables
	}
	
	public Vector3 getPosition() {
		return this.transform.localPosition;
	}
	
	
	
	
	//from ballcollision by Julia
	//Andreas moved code here
	
	bool isVisible = false;
	ArrayList allWalls;
	
	public GUIStyle myStyle;
	
	/*void OnCollisionEnter(Collision collision) {
		
		//Debug.Log("This collider collided with: " + collision.contacts[0].otherCollider.name);
		
		// Change name the objects name depending on what we want the wall to react with
		if(collision.contacts[0].otherCollider.name.Equals("PowerUp(Clone)")) {
			
		}
    }*/
	
	public void startDestroyWallsPowerup() {
		isVisible = true;
		print("POWER UP YEAHHEHEHEHEHMGMGMHGJHDGLA OMFG!!! EHHEHE");
			
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

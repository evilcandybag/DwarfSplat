using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour, IActor {
	private GameObject dText;
	public AudioClip DESTRUCTION_MODE;
	IItem inventory;
	// Use this for initialization
	void Start () {
		dText = GameObject.Find("DText");
		if (dText == null)
		{
			Debug.Log("DText object not found!");
		}
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
	
	/*void OnCollisionEnter(Collision collision) {
		
		//Debug.Log("This collider collided with: " + collision.contacts[0].otherCollider.name);
		
		// Change name the objects name depending on what we want the wall to react with
		if(collision.contacts[0].otherCollider.name.Equals("PowerUp(Clone)")) {
			
		}
    }*/
	
	public void startDestroyWallsPowerup() {
		dText.GetComponent<TextScript>().setVisible();
		gameObject.GetComponent<AudioSource>().PlayOneShot(DESTRUCTION_MODE);
		
		isVisible = true;
		//print("POWER UP YEAHHEHEHEHEHMGMGMHGJHDGLA OMFG!!! EHHEHE");
			
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
		dText.GetComponent<TextScript>().setInvisible();		
	}

}

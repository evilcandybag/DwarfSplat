using UnityEngine;
using System.Collections;

public class DwarfSplatter : MonoBehaviour {
	private int score;
	private AudioSource aus;
	private DwarfManager dm;
	
	public GameObject splatter;
	
	public AudioClip splat1;
	public AudioClip splat2;
	
	public GUIStyle style = new GUIStyle();
	
	void Awake() {
		score = 0;
	}
	// Use this for initialization
	void OnCollisionEnter(Collision collision) {
		if (aus == null) {
			aus = gameObject.GetComponent<AudioSource>();
		}
		// check if this is a dwarf. 
		GameObject col = collision.contacts[0].otherCollider.gameObject;
		Dwarf d = col.GetComponent<Dwarf>();
		if (d != null) {
			if (Random.value > 0.5) {
				aus.PlayOneShot(splat1);
			} else {
				aus.PlayOneShot(splat2);
			}
			var pos = d.getPosition();
			Debug.Log("" + pos);
			var splat = Instantiate(splatter, new Vector3(pos.x,0.21f,pos.z), Quaternion.identity);
			Debug.Log(splat == null);
			Destroy(splat,3);
			
			ActorController.getActorController().destoyActor(d);
			score++;
		}
	}
	
	void OnGUI () {
		
		//style.fontSize = 35;
		//style.normal.textColor = Color.white;
		
		GUI.Box(new Rect(10f, 10f, 400f,40f), 
			"SCORE: " + score + "\t\t\t " +
			"DWARVES: " + ActorController.getActorController().getDwarfActors().Count, style);
		
	}
}

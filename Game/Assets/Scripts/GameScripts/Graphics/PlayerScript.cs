using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	
	public static int tiltVertical;
	public static int tiltHorizontal;
	public TerrainDeformer terr;
	public int force = 10;
	
	bool isDown;
	// Use this for initialization
	void Start () {
		tiltVertical = 0;
		tiltHorizontal = 0;
		InvokeRepeating("callTerrainDestruction", 0, 0.05f);

	}
	
	void FixedUpdate () {
		tiltHorizontal = 0;
		tiltVertical = 0;
		if(Input.GetKey ("down")){
			rigidbody.AddForce(0,10,force);
			tiltVertical = -1;
			isDown = true;
		}
		else if(Input.GetKey ("up")){
			rigidbody.AddForce(0,10,-force);
			tiltVertical = 1;
			isDown = true;

		}
		if(Input.GetKey ("left")){
			rigidbody.AddForce(force,10,0);
			tiltHorizontal = -1;
			isDown = true;

		}
		else if(Input.GetKey ("right")){
			rigidbody.AddForce(-force,10,0);
			tiltHorizontal = 1;
			isDown = true;
		}
		else{
			isDown = false;
		}
	}
	void callTerrainDestruction(){
		if(isDown) {
		//	print ("terrain");
			GameObject obj = GameObject.Find("Terrain");
			TerrainDeformer terr =(TerrainDeformer)obj.GetComponent<TerrainDeformer>();
		//	if(!terr.isGrassTexture){
			terr.DestroyTerrain(transform.position, 1.0f);
		//	}
		}
	}
}

using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	
	public static int tiltVertical;
	public static int tiltHorizontal;
	public TerrainDeformer terr;
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
			rigidbody.AddForce(0,0,6);
			tiltVertical = -1;
			isDown = true;
		}
		else if(Input.GetKey ("up")){
			rigidbody.AddForce(0,0,-6);
			tiltVertical = 1;
			isDown = true;

		}
		else if(Input.GetKey ("left")){
			rigidbody.AddForce(6,0,0);
			tiltHorizontal = -1;
			isDown = true;

		}
		else if(Input.GetKey ("right")){
			rigidbody.AddForce(-6,0,0);
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

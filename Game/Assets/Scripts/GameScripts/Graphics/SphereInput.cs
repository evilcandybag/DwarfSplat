using UnityEngine;
using System.Collections;

public class SphereInput : MonoBehaviour {
	public float forwardSpeed = 60.9f;
	public TerrainDeformer terr;
	bool isDown;
	Color colorStart = Color.white;
 	Color colorEnd = Color.green;
 	float duration = 10.0f;
	float alpha;
	Color color;
	float t;

	// Use this for initialization
	void Start () {
	   	InvokeRepeating("callTerrainDestruction", 0, 0.2f);

		InvokeRepeating("fadeOut", 0, 2.2f);

		colorStart = renderer.material.color;
  	    colorEnd = new Color(colorStart.r, colorStart.g, colorStart.b, 0.0f);
	}
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.U)){
			gameObject.rigidbody.AddForce(Vector3.forward * 20);
			//transform.position += transform.forward * forwardSpeed * Time.deltaTime;
			isDown = true;
		}
		else if(Input.GetKey(KeyCode.K)){
			gameObject.rigidbody.AddForce(Vector3.right * 40);
		//	transform.position += transform.forward * forwardSpeed* Time.deltaTime;
			isDown = true;
		}
		else if(Input.GetKey(KeyCode.H)){
			gameObject.rigidbody.AddForce(Vector3.left * 40);
		//	transform.position += transform.forward * forwardSpeed* Time.deltaTime;
			isDown = true;
		}
		else if(Input.GetKey(KeyCode.J)){
			gameObject.rigidbody.AddForce(Vector3.back * 20);
		//	transform.position += transform.forward * forwardSpeed* Time.deltaTime;
			isDown = true;
		}
		else{
			isDown = false;
		}
		if(Input.GetKey(KeyCode.H)){
			transform.position -= transform.forward *Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.N)){
			callSnowTexture();
		}
		if(Input.GetKey(KeyCode.F)){
			//fadeOut();
		}
	}
	void callSnowTexture(){
			GameObject obj = GameObject.Find("Terrain");
			TerrainDeformer terr =(TerrainDeformer)obj.GetComponent<TerrainDeformer>();
			terr.changeGrassTerrain();
	}
	
	void callTerrainDestruction(){
		if(isDown) {
			GameObject obj = GameObject.Find("Terrain");
			TerrainDeformer terr =(TerrainDeformer)obj.GetComponent<TerrainDeformer>();
			terr.DestroyTerrain(transform.position, 1.1f);
		}
	}
	void fadeOut(){ 
		
		for (t = 0; t < duration; t += Time.deltaTime) {
		   renderer.material.color = Color.Lerp (colorStart, colorEnd, t/duration);
		}
	//	alpha = Mathf.Lerp(alpha,0,Time.deltaTime*5);
	//			color.a = alpha;
	//	float lerp = Mathf.Lerp (0,155,Time.time);
				//colorEnd.a -= 0.01f;
		//float lerp = Mathf.PingPong (Time.time, duration) / duration;
     //	renderer.material.color = Color.Lerp (colorStart, colorEnd, Time.time);
	}

}

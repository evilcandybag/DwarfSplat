using UnityEngine;
using System.Collections;

public class MapGenerationScript : MonoBehaviour {
	
	
	public GameObject wall;

	void Start() {
		GameObject obj = Instantiate(wall, new Vector3(0, 1, 0), Quaternion.identity);
		//GameObject test = (GameObject) obj as GameObject;
		obj.transform.Rotate(new Vector3(0,90,0));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

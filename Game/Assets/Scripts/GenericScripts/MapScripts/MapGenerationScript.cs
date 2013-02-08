using UnityEngine;
using System.Collections;

public class MapGenerationScript : MonoBehaviour {
	
	
	public Transform wall;

	void Start() {
		Instantiate(wall, new Vector3(0, 1, 0), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

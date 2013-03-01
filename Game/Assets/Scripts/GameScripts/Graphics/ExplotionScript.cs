using UnityEngine;
using System.Collections;

/// <summary>
/// Explotion script.
/// Julia Adamsson 2013
/// </summary>
public class ExplotionScript : MonoBehaviour {

	public GameObject explotion;
	
	public void Explode(Vector3 position){
		 Instantiate(explotion, position, Quaternion.identity);
	}
}

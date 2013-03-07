using UnityEngine;
using System.Collections;

/// <summary>
/// Explotion script.
/// Julia Adamsson 2013
/// </summary>
public class ExplotionScript : MonoBehaviour {

	public GameObject explotion;
	public AudioClip expl1;
	public AudioClip expl2;
	
	private AudioSource aus;
	
	public void Explode(Vector3 position){
		if (aus == null) {
			//horrible =(
			aus = GameObject.Find("Ball(Clone)").GetComponent<AudioSource>();
		}
		if (Random.value > 0.5) {
			aus.PlayOneShot(expl1,1);
		} else {
			aus.PlayOneShot(expl2,1);
		} 
		Instantiate(explotion, position, Quaternion.identity);
	}
}

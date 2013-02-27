using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {
	
	public Transform target;
	
	// Use this for initialization
	void Start () {
		GetComponent<MovementAgent>().MoveTo(target.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		
		
		
	}
	
}

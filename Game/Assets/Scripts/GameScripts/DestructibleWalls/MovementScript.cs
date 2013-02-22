using UnityEngine;
using System.Collections;

/// <summary>
/// Used to steer the cube..
/// </summary>

public class MovementScript : MonoBehaviour {
	
	public float moveSpeed = 2;
	public float rotateSpeed = 10;
	
	void FixedUpdate () 
	{		
		if (Input.GetKey (KeyCode.LeftArrow)){
	    	rigidbody.AddForce (Vector3.left * 25);
		}
		if (Input.GetKey (KeyCode.RightArrow)){
	   	 	rigidbody.AddForce (Vector3.right * 25);
		}
		if (Input.GetKey (KeyCode.UpArrow)){
	    	rigidbody.AddForce (Vector3.forward * 25);
		}
		if (Input.GetKey (KeyCode.DownArrow)){
	   	 	rigidbody.AddForce (-Vector3.forward * 25);
		}
		if (Input.GetKey (KeyCode.Space)){
	   	 	transform.position = new Vector3(4f, 3f, -16f);
		}
		
	}
}

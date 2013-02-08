using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {
	
	public float speed = 250;
	
	protected CharacterController controller;
	
	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
		float effectiveSpeed = speed * Time.deltaTime;
		Vector3 direction = Vector3.zero;
		if (Input.GetKey(KeyCode.UpArrow)) {
			direction = Vector3.forward;
		}
		if (Input.GetKey(KeyCode.DownArrow)) {
			direction = Vector3.back;
		}
		if (Input.GetKey(KeyCode.RightArrow)) {
			direction = Vector3.right;
		}
		if (Input.GetKey(KeyCode.LeftArrow)) {
			direction = Vector3.left;
		}
		
		controller.SimpleMove(direction * effectiveSpeed);
		
	}
}

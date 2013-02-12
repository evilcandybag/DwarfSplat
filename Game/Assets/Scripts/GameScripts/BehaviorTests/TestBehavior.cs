using UnityEngine;
using System.Collections;
using BehaviorTrees;

public class TestBehavior : MonoBehaviour {
	
	private bool red = true;
	
	// Use this for initialization
	void Start () {
		InvokeRepeating("ChangeColor", .01f,1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	void ChangeColor() {
		if (red) {
			renderer.material.color = Color.red;
		} else {
			renderer.material.color = Color.blue;
		}
		red = !red;
	}
	
	Node root = new PrioritySelector();
}

using UnityEngine;
using System.Collections;
using BehaviorTrees;

public class ColorSwitcher : MonoBehaviour {
	
	private bool red = true;
	private PrioritySelector.PriorityNode redPrio, bluePrio;
	private PrioritySelector root;
	private Action redder, bluer;

	public float updatespeed;
	// Use this for initialization
	void Start () {
		updatespeed = 0.05f;
		root = new PrioritySelector();
		redder = new Action(Redder);
		bluer = new Action(Bluer);
		redder.name = "redder"; bluer.name = "bluer";
		
		redPrio = root.AddChild(redder,0.0); bluePrio = root.AddChild(bluer,100.0);
		
		//InvokeRepeating("MyUpdate", .01f,updatespeed);
		
	}
	
	void MyUpdate() {
		root.Visit();
	}
	
	// Update is called once per frame
	void Update () {
		//Log("Update");
		//root.Visit();
		root.Visit ();
	}
		
	void Log(string s) {
		Debug.Log(s + " redPrio: " + redPrio.Prio + ", bluePrio: " + bluePrio.Prio);
	}
	
	void ChangeColor() {
		if (red) {
			renderer.material.color = Color.red;
		} else {
			renderer.material.color = Color.blue;
		}
		red = !red;
	}
	
	/// <summary>
	/// Turn a node redder until entirely red.
	/// Simulates a "rest" type behavior.
	/// </summary>
	Node.Status Redder() {
		float r = 1, b = 0;
		if (renderer.material.color.r < 1)
			r = renderer.material.color.r + 0.01f;
		if (renderer.material.color.b > 0)
			b = renderer.material.color.b - 0.01f;
		var col = new Color(r,0,b);
		renderer.material.color = col;
		if (redPrio.Prio > 0) {
			redPrio.Prio -= 1;
			return Node.Status.RUNNING;
		} else {
			return Node.Status.SUCCESS;
		}
	}
	
	/// <summary>
	/// Turns a node bluer. Each pass increases the need to be red.
	/// This will be the "default" behavior.
	/// </summary>
	Node.Status Bluer() {
		float r = 0, b = 1;
		if (renderer.material.color.r > 0)
			r = renderer.material.color.r - 0.01f;
		if (renderer.material.color.b < 1)
			b = renderer.material.color.b + 0.01f;
		var col = new Color(r,0,b);
		renderer.material.color = col;
		redPrio.Prio += 1;
		return Node.Status.SUCCESS;
	}
}

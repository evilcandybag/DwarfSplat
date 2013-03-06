using UnityEngine;
using System.Collections;

/// <summary>
/// Menu script.
/// Julia Adamsson 2013
/// </summary>
public class MenuScript : MonoBehaviour {
	
	public bool quit = false;
	
	void OnMouseEnter() {
		renderer.material.color = Color.blue;	
	}
	
	void OnMouseExit() {
		renderer.material.color = Color.white;	
	}
	
	void OnMouseUp() {
		
		if(quit){
			Application.Quit();	
		}
		else{
			Application.LoadLevel("Behavior+MazeTest");
		}
		
	}

}

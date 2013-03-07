using UnityEngine;
using System.Collections;

public class GameSceneMenuScript : MonoBehaviour {
	
	public GUIStyle style = new GUIStyle();
	
	// Update is called once per frame
	void OnGUI () {
		Rect menuRect = new Rect(Screen.width-110f, 10f, 100f,50f);
		
		if(GUI.Button(menuRect, "MENU",style)){
			GameObject.Find("Terrain").GetComponent<TerrainPath>().RunBackUpTerrain();
			Application.LoadLevel("MenuScene");
		}
		
	}
}

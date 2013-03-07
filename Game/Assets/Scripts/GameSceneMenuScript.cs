using UnityEngine;
using System.Collections;

public class GameSceneMenuScript : MonoBehaviour {

	// Update is called once per frame
	void OnGUI () {
		if(GUI.Button(new Rect(Screen.width-130f, 30f, 100f,50f), "Main Menu")){
			GameObject.Find("Terrain").GetComponent<TerrainPath>().RunBackUpTerrain();
			Application.LoadLevel("MenuScene");
		}
	}
}

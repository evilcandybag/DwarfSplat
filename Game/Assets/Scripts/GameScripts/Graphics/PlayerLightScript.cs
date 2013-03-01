using UnityEngine;
using System.Collections;

public class PlayerLightScript : MonoBehaviour {
	GameObject player;
	
	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		if (player == null)
		{
			Debug.Log("Player object not found!");
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(player.transform.position.x, player.transform.position.y+1f, player.transform.position.z);		
	}
}

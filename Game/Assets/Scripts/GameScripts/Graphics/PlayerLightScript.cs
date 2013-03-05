using UnityEngine;
using System.Collections;

public class PlayerLightScript : MonoBehaviour {
	GameObject player;
	
	// Use this for initialization
	void Start () {
		player = GameObject.Find("Ball(Clone)");
		if (player == null)
		{
			Debug.Log("Player object not found!");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (player == null)
			player = GameObject.Find("Ball(Clone)");
		transform.position = new Vector3(player.transform.position.x, player.transform.position.y+1f, player.transform.position.z);		
	}
}

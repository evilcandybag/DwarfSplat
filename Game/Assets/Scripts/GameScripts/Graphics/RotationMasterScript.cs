using UnityEngine;
using System.Collections;

public class RotationMasterScript : MonoBehaviour {
	
	public GameObject player;	
	public Quaternion desiredTilt;
	public float animationSpeed;
	public float perspectiveAngle;
	public float cameraDistance;
	
	public GUIStyle myStyle;

	void Start () {
		player = GameObject.Find("Cube");
		if (player == null)
		{
			Debug.Log("Player object not found!");
		}
		cameraDistance = 5.0f;
		animationSpeed = 1.0f;
	}
	
	void LateUpdate () {
		tiltPlane();
		positionPlane();
	}
	
	void tiltPlane()
	{
		desiredTilt = Quaternion.Euler(PlayerScript.tiltVertical*30.0f + perspectiveAngle, 0 ,-PlayerScript.tiltHorizontal*30.0f);
		transform.rotation = Quaternion.Slerp(transform.rotation, desiredTilt, Time.deltaTime*animationSpeed*2f);
	}
	
	void positionPlane()
	{
		transform.position = player.transform.position;
	}
	
	 void OnGUI() {
        perspectiveAngle = GUI.VerticalSlider(new Rect(25, 25, 100, 100), perspectiveAngle, 0.0F, 45.0F);
		animationSpeed = GUI.VerticalSlider(new Rect(25, 150, 100, 100), animationSpeed, 5F, 0.5F);
		cameraDistance = GUI.VerticalSlider(new Rect(25, 275, 100, 100), cameraDistance, 3F, 20F);
		GUI.Label(new Rect(45, 20, 1000, 20), "Top-down perspective");
		GUI.Label(new Rect(45, 110, 1000, 20), "Follow perpective");
		GUI.Label(new Rect(45, 145, 1000, 20), "Fast camera");
		GUI.Label(new Rect(45, 235, 1000, 20), "Slow camera");
		GUI.Label(new Rect(45, 270, 1000, 20), "Near view");
		GUI.Label(new Rect(45, 360, 1000, 20), "Far view");
		GUI.Label(new Rect(20, 20, 1000, 20), "Use arrow keys to tilt the world and move the ball,\nadjust different camera properties by using the sliders on the left.", myStyle);		
    }
}

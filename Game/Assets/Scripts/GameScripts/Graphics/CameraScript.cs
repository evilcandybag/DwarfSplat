using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
	public GameObject player;
	public GameObject rotationMaster;
	
	public Vector3 desiredPosition;
    public Vector3 nextPosition;
	
	public float cameraDistance;
	public float angle;
			
	void Start () {
		player = GameObject.Find("Player");
		if (player == null)
		{
			Debug.Log("Player object not found!");
		}
		rotationMaster = GameObject.Find("Rotation Master");
		if (rotationMaster == null)
		{
			Debug.Log("Rotation Master object not found!");
		}
	}
	
	void LateUpdate() {
		cameraDistance = rotationMaster.GetComponent<RotationMasterScript>().cameraDistance;
		moveCamera();
	}
		
	void moveCamera()
	{
		desiredPosition = new Vector3(
			player.transform.position.x + cameraDistance*Mathf.Sin(PlayerScript.tiltHorizontal*(angle*(Mathf.PI/180))),	//X-coordinate
			player.transform.position.y + cameraDistance - Mathf.Abs(Mathf.Sin(PlayerScript.tiltHorizontal*(angle*(Mathf.PI/180)))) - Mathf.Abs(Mathf.Sin(PlayerScript.tiltVertical*(angle*(Mathf.PI/180)))) - 0.5f*Mathf.Sin(rotationMaster.GetComponent<RotationMasterScript>().perspectiveAngle*(Mathf.PI/180))*cameraDistance,	//Y-coordinate
			player.transform.position.z + cameraDistance*Mathf.Sin(PlayerScript.tiltVertical*(angle*(Mathf.PI/180))) + Mathf.Sin(rotationMaster.GetComponent<RotationMasterScript>().perspectiveAngle*(Mathf.PI/180))*cameraDistance);	//Z-coordinate
    	nextPosition = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime*rotationMaster.GetComponent<RotationMasterScript>().animationSpeed);
		transform.position = new Vector3(nextPosition.x, nextPosition.y, nextPosition.z);
	}
}

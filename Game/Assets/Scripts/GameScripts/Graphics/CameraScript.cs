using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
	public GameObject player;
	public GameObject rotationMaster;
	
	public Vector3 desiredPosition;
    public Vector3 nextPosition;
	
	public float cameraDistance;
	public float angle;
	
	//Shaky stuff
	public float shakeRemaining;
	public float decrement = 3.0f;
	public float amount = 0.02f;
	
	public Texture aTexture;
			
	void Start () {
		player = GameObject.Find("Ball(Clone)");
		if (player == null)
		{
			Debug.Log("Player object not found!");
		}
		rotationMaster = GameObject.Find("RotationMaster");
		if (rotationMaster == null)
		{
			Debug.Log("Rotation Master object not found!");
		}
	}
	
	void LateUpdate() {
		if (Input.GetKeyDown("f")){
			Shake();
		}
		angle = rotationMaster.GetComponent<RotationMasterScript>().tiltAngle;
		cameraDistance = rotationMaster.GetComponent<RotationMasterScript>().cameraDistance;
		moveCamera();
	}
		
	void moveCamera() {
		desiredPosition = new Vector3(
			player.transform.position.x + cameraDistance*Mathf.Sin(PlayerScript.tiltHorizontal*(angle*(Mathf.PI/180))),	//X-coordinate
			player.transform.position.y + cameraDistance*(Mathf.Cos(-1f*PlayerScript.tiltVertical*(angle*(Mathf.PI/180)) - rotationMaster.GetComponent<RotationMasterScript>().perspectiveAngle*(Mathf.PI/180)))*(Mathf.Cos(-1f*PlayerScript.tiltHorizontal*(angle*(Mathf.PI/180)))), //player.transform.position.y + cameraDistance*(Mathf.Cos(PlayerScript.tiltVertical*angle)-rotationMaster.GetComponent<RotationMasterScript>().perspectiveAngle*(Mathf.PI/180))*Mathf.Cos(PlayerScript.tiltHorizontal*angle)) , //- Mathf.Abs(Mathf.Sin(PlayerScript.tiltHorizontal*(angle*(Mathf.PI/180)))) - Mathf.Abs(Mathf.Sin(PlayerScript.tiltVertical*(angle*(Mathf.PI/180)))) - 0.5f*Mathf.Sin(rotationMaster.GetComponent<RotationMasterScript>().perspectiveAngle*(Mathf.PI/180))*cameraDistance,	//Y-coordinate
			player.transform.position.z + cameraDistance*Mathf.Sin(PlayerScript.tiltVertical*(angle*(Mathf.PI/180))) + Mathf.Sin(rotationMaster.GetComponent<RotationMasterScript>().perspectiveAngle*(Mathf.PI/180))*cameraDistance);	//Z-coordinate
    	nextPosition = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime*rotationMaster.GetComponent<RotationMasterScript>().animationSpeed);
		DoShake();
		transform.position = nextPosition;
	}
	
	void DoShake() {
		if (shakeRemaining == 0) {
			return;
		}
		else if (shakeRemaining < 0.001f)
			shakeRemaining = 0;
		else {
			shakeRemaining -= decrement*shakeRemaining*Time.deltaTime;
			nextPosition = new Vector3(nextPosition.x+Random.Range(-amount*shakeRemaining, amount*shakeRemaining), nextPosition.y+Random.Range(-amount*shakeRemaining, amount*shakeRemaining), nextPosition.z+Random.Range(-amount*shakeRemaining, amount*shakeRemaining));
		}
	}
	
	public void Shake() {
		shakeRemaining = 20.0f;
	}
}

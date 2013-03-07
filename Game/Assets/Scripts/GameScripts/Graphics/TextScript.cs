using UnityEngine;
using System.Collections;

public class TextScript : MonoBehaviour {
	Vector3 point;
	bool isVisible;
	
	// Use this for initialization
	void Start () {
		renderer.material.color = Color.red;
		setInvisible();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if(isVisible) {
			//transform.rotation = Quaternion.Euler(90,0,180);
			transform.LookAt(Camera.main.transform);
			transform.rotation = Quaternion.Euler(-1f*transform.rotation.eulerAngles.x,180,-1f*transform.rotation.eulerAngles.z);
			point = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.8f, 3.0f));
			transform.position = Vector3.Lerp(transform.position, point, Time.deltaTime*20);
		}
	}
	
	public void setVisible() {
		renderer.enabled = true;
		isVisible = true;
	}
	
	public void setInvisible() {
		renderer.enabled = false;
		isVisible = false;
	}
	
}

using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public static int tiltVertical;
    public static int tiltHorizontal;
    public TerrainPath terr;
    public int force = 10;
	
	private int time = 0;

    bool isDown;
    // Use this for initialization
    void Start() {
        tiltVertical = 0;
        tiltHorizontal = 0;
        //	InvokeRepeating("callTerrainDestruction", 0, 0.5f);

    }

    void FixedUpdate() {
        if (rigidbody.transform.position.y < 1.3) {
            tiltHorizontal = 0;
            tiltVertical = 0;
            if (Input.GetKey("down")) {
                rigidbody.AddForce(0, 10, force);
                tiltVertical = -1;
                //	isDown = true;
            }
            else if (Input.GetKey("up")) {
                rigidbody.AddForce(0, 10, -force);
                tiltVertical = 1;
                //	isDown = true;
                //	callTerrainDestruction();

            }
            if (Input.GetKey("left")) {
                rigidbody.AddForce(force, 10, 0);
                tiltHorizontal = -1;
                //	isDown = true;
                //	callTerrainDestruction();
            }
            else if (Input.GetKey("right")) {
                rigidbody.AddForce(-force, 10, 0);
                tiltHorizontal = 1;
                //	isDown = true;
                //	callTerrainDestruction();
            }
            /*else{
                isDown = false;
            }*/
			time++;
			if(time >= 3) {
				callTerrainDestruction();
				time = 0;
			}
			
			//callTerrainDestruction();
			

        }
    }
    void callTerrainDestruction() {
        //	if(isDown) {
        GameObject obj = GameObject.Find("Terrain");
        //	print(transform.position);
        TerrainPath terr = (TerrainPath)obj.GetComponent<TerrainPath>();
        terr.DestroyTerrain(transform.position);
        //	}
    }
}

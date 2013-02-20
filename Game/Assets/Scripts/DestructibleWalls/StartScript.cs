using UnityEngine;
using System.Collections;

public class StartScript : MonoBehaviour {

	GameObject maze;
	// Use this for initialization
	void Start () {
				
		
		WallMeshManagerScript cms = GetComponent<WallMeshManagerScript>();
		
		float angle = 0f;
					
		float x = -10f;
		float y = 0f;
		float z = -10f;
		
		for(int i = 0; i < 3; i++ ){
			
			Vector3 p0 = new Vector3(  -10f,	0.01f,		0.5f );
			Vector3 p1 = new Vector3(	10f, 	0.01f,		0.5f );
			Vector3 p2 = new Vector3( 	10f, 	0.01f,	   -0.5f );
			Vector3 p3 = new Vector3(  -10f,	0.01f, 	   -0.5f );	
			 
			Vector3 p4 = new Vector3(  -10f,	10f, 		0.5f );
			Vector3 p5 = new Vector3( 	10f, 	10f, 		0.5f );
			Vector3 p6 = new Vector3( 	10f,	10f,       -0.5f );
			Vector3 p7 = new Vector3(  -10f,	10f,       -0.5f );
			
			maze = cms.CreateWallwithVertices(p0,p1,p2,p3,p4,p5,p6,p7);
			
			Debug.Log(angle);

			maze.transform.rotation = Quaternion.Euler(0f, angle, 0f);
			maze.transform.position = new Vector3(x, y, z);
			angle += 90f;
			x+=10f;
			z+=10f;
			
		}	
	}
	
	void OnGUI(){
		GUI.color = Color.white;
		GUI.Box(new Rect(10,10,350,175), "");
		GUI.Label(new Rect(25,25,500,500), "Steer the ball with the arrow keys");
		GUI.Label(new Rect(25,50,500,500), "Press 1 in the beginning if you want the walls ");
		GUI.Label(new Rect(25,75,500,500), "to be destroyed in smaller pieces..");
		GUI.Label(new Rect(25,100,500,500), "Space might work to bring back the ball if it flies away! ;)");
		GUI.Label(new Rect(25,125,500,500), "Otherwise refresh the web browser to play again..");
		GUI.Label(new Rect(25,150,500,500), "HAPPY DESTRUCTION!");
	}
}


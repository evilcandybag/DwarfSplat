using UnityEngine;
using System.Collections;

public class MapGenerationScript : MonoBehaviour {
	
	
	GameObject wall;
	WallMeshManagerScript cms;
	
	int[,] maze;
	int size;
	
	int bot = 0, right = 1;
	
	void Start() {
	//	GameObject tmp;
		
		cms = GetComponent<WallMeshManagerScript>();
		
		size = 20;
		
		generateMaze();
		for(int i = 0; i < size*size; i++) {
			
			if (!connectedBot(i)) {
				wall = CreateMazePart();
				wall.layer = LayerMask.NameToLayer("Obstacles");
				//tmp = (GameObject) Instantiate(wall, getPosition(i, bot), Quaternion.identity);
				//tmp.transform.Rotate(new Vector3(0,90,0));
				wall.transform.position = getPosition(i, bot);
				wall.transform.rotation = Quaternion.identity;
			}
			if (!connectedRight(i)) {
				wall = CreateMazePart();
				wall.layer = LayerMask.NameToLayer("Obstacles");
				//tmp = (GameObject) Instantiate(wall, getPosition(i, right), Quaternion.identity);
				wall.transform.position = getPosition(i, right);
				wall.transform.rotation = Quaternion.identity;
				wall.transform.Rotate(new Vector3(0,90,0));
			}
		}
		
		GameObject go = GameObject.Find("emptyCreationStuff");
		//go.GetComponent<TileGraphGenerator>().Scan();
		//Destroy(wall);
	}
	
	private GameObject CreateMazePart() {
		
		// The size of the wall part will be (1,1,0.1)
		Vector3 p0 = new Vector3(  -0.5f,	-0.5f,		0.05f );
		Vector3 p1 = new Vector3(	0.5f, 	-0.5f,		0.05f );
		Vector3 p2 = new Vector3( 	0.5f, 	-0.5f,	   -0.05f );
		Vector3 p3 = new Vector3(  -0.5f,	-0.5f, 	-0.05f );	
		 
		Vector3 p4 = new Vector3(  -0.5f,	0.5f, 	0.05f );
		Vector3 p5 = new Vector3( 	0.5f, 	0.5f, 	0.05f );
		Vector3 p6 = new Vector3( 	0.5f,	0.5f,    -0.05f );
		Vector3 p7 = new Vector3(  -0.5f,	0.5f,    -0.05f );
		
		return cms.CreateWallwithVertices(p0,p1,p2,p3,p4,p5,p6,p7);
	}
	
	private Vector3 getPosition(int tile, int placement) {
		float y = 0.5f;
		float x = 0;
		float z = 0;
		
		int k = size/2;
		int tx = tile%size;
		int tz = -(tile/size);
		if (placement == bot) {
			x = tx-(k-.5f);
			z = tz+k-1;
		}
		else { //right
			x = tx-k+1;
			z = tz+k-.5f;
		}
		
		return(new Vector3(x,y,z));
	}
	
	private bool connectedBot(int tile){
		if (tile >= size*(size-1)) 			//allready walled bot
			return true;
		else if (maze[tile, tile+size] == 0) { // not connected, wall bot
			return false;
		}
		else
			return true;					//connected, do not wall
	}
	
	private bool connectedRight(int tile){
		if (tile%size == size-1) 			//allready walled right
			return true;
		else if (maze[tile, tile+1] == 0) 	// not connected, wall right
			return false;
		else 
			return true;					//Connected, do not wall
	}
	
	private void generateMaze(){ //using Recursive backtracker algorithm, for no apparent reason
		
		int nrNodes = size*size;
		int[] nodes = new int[nrNodes];  
		
		for (int i = 0; i < nrNodes; i++){ //setting all nodes to un-made (1)
			nodes[i] = 1;
		}
		
		maze = new int[nrNodes, nrNodes];
		
		for(int i = nrNodes; i < nrNodes; i++) { //adding edges
			for(int j = nrNodes; j < nrNodes; j++) {
				maze[i,j] = 1;
			}
		}
		
		System.Random rng = new System.Random(); 
		//time to remove them edges
		
		Stack stack = new Stack();
		
		stack.Push(0);
		nodes[0] = 0; //is made!
		ArrayList unmadeNodes = new ArrayList(); //could probably be solved more efficiently if needed!
		while(stack.Count > 0) {
			int node = (int) stack.Pop();
			
			if(node >= size && nodes[node-size]==1) { //check the node above, considering if it is already on the top.
				unmadeNodes.Add(node-size);
			}
			
			if(node < size*size-size && nodes[node+size]==1) {  //check the node below, considering if it is already on the bot.
				unmadeNodes.Add(node+size);
			}
			
			if(node%size != 0 && nodes[node-1]==1) {  //check the node to the left, considering if it is already to the left.
				unmadeNodes.Add(node-1);
			}
			
			if(node%size != size-1 && nodes[node+1]==1) {  //check the node to the right, considering if it is already to the right.
				unmadeNodes.Add(node+1);
			}
			
			if(unmadeNodes.Count > 0) {
				stack.Push(node);
				
				int next = rng.Next(unmadeNodes.Count);
				stack.Push((int) unmadeNodes[next]);
				
				maze[node, (int) unmadeNodes[next]] = 1; //make a clear path!
				maze[(int) unmadeNodes[next], node] = 1;
				
				nodes[(int) unmadeNodes[next]] = 0; //is made!
				
				unmadeNodes = new ArrayList();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
}

using UnityEngine;
using System.Collections;


/// <summary>
/// Manages the creation of all the walls as well as the destruction of them
/// Julia Adamsson 2013
/// </summary>

public class WallMeshManagerScript : MonoBehaviour {
	
	// Set the material in the editor
	public Material wallMaterial;
	// All whole walls are in this list
	public ArrayList wallList;
	
	void Awake(){
		wallList = new ArrayList();
	}

	// Used to create the whole walls
	// This method is based on the box script at this wiki: http://wiki.unity3d.com/index.php/ProceduralPrimitives
	// Though this method uses vertex points instead of lengths. Since this is the method where the walls are created
	// the first time, it would be a point to just specify a length, since the points are in local space.
	// Which means I send in the same vertex points for each wall part, and then move them with their transform
	// This wall object is removed and replaced with the "destroyed wall" (wall parts)
	// Returns the object that is created, these could be added in a list with wall so that we can iterate over them
	// and for example add the wallCollisionScript, since they should not always we destroyable.
	public GameObject CreateWallwithVertices(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, Vector3 p5, Vector3 p6, Vector3 p7){
		
		// Create gameObject that is going to represent this wall part
		GameObject wall = new GameObject("Wall");
		Mesh mesh = new Mesh();
		wall.AddComponent<MeshFilter>();
		wall.AddComponent<MeshRenderer>();
		 
		// Adding them to veritices array in thier "block" pieces to be able
		// to use them easier later on. Consider not using bottom since it
		// will not be visible
		Vector3[] vertices = new Vector3[]
		{
			// Bottom
			p0, p1, p2, p3,
		 
			// Left
			p7, p4, p0, p3,
		 
			// Front
			p4, p5, p1, p0,
		 
			// Back
			p6, p7, p3, p2,
		 
			// Right
			p5, p6, p2, p1,
		 
			// Top
			p7, p6, p5, p4
		};
		
		// Fixing normals, so that the front side is facing the camera and we can see
		// the wall without it being backface culled
		Vector3 up 		= Vector3.up;
		Vector3 down 	= Vector3.down;
		Vector3 front 	= Vector3.forward;
		Vector3 back 	= Vector3.back;
		Vector3 left 	= Vector3.left;
		Vector3 right 	= Vector3.right;
		 
		Vector3[] normales = new Vector3[]
		{
			// Bottom
			down, down, down, down,
		 
			// Left
			left, left, left, left,
		 
			// Front
			front, front, front, front,
		 
			// Back
			back, back, back, back,
		 
			// Right
			right, right, right, right,
		 
			// Top
			up, up, up, up
		};
		
		// UV-mapping so that the texture will look good
		Vector2 _00 = new Vector2( 0f, 0f );
		Vector2 _10 = new Vector2( 1f, 0f );
		Vector2 _01 = new Vector2( 0f, 1f );
		Vector2 _11 = new Vector2( 1f, 1f );
		 
		Vector2[] uvs = new Vector2[]
		{
			// Bottom
			_11, _01, _00, _10,
		 
			// Left
			_11, _01, _00, _10,
		 
			// Front
			_11, _01, _00, _10,
		 
			// Back
			_11, _01, _00, _10,
		 
			// Right
			_11, _01, _00, _10,
		 
			// Top
			_11, _01, _00, _10,
		};
		
		// Adding the tringles that will form the different parts of the wall
		// The array has length 24 and the same values återkommer
		int[] triangles = new int[]
		{
			// Bottom
			3, 1, 0,
			3, 2, 1,			
		 
			// Left
			3 + 4 * 1, 1 + 4 * 1, 0 + 4 * 1,
			3 + 4 * 1, 2 + 4 * 1, 1 + 4 * 1,
		 
			// Front
			3 + 4 * 2, 1 + 4 * 2, 0 + 4 * 2,
			3 + 4 * 2, 2 + 4 * 2, 1 + 4 * 2,
		 
			// Back
			3 + 4 * 3, 1 + 4 * 3, 0 + 4 * 3,
			3 + 4 * 3, 2 + 4 * 3, 1 + 4 * 3,
		 
			// Right
			3 + 4 * 4, 1 + 4 * 4, 0 + 4 * 4,
			3 + 4 * 4, 2 + 4 * 4, 1 + 4 * 4,
		 
			// Top
			3 + 4 * 5, 1 + 4 * 5, 0 + 4 * 5,
			3 + 4 * 5, 2 + 4 * 5, 1 + 4 * 5,
		 
		};
		 
		mesh.vertices = vertices;
		mesh.normals = normales;
		mesh.uv = uvs;
		mesh.triangles = triangles;
		 
		mesh.RecalculateBounds();
		mesh.Optimize();
		
		// Rigidbody not needed for the walls when they're just standing
		// and being.. walls.. :)
		//wall.AddComponent<Rigidbody>();
		wall.GetComponent<MeshFilter>().mesh = mesh;
		
		// If I want to set Material from code, the material has to be in the resources folder
		//wallMaterial = Resources.Load("WallMaterial", typeof(Material)) as Material;
		
		wall.renderer.material = wallMaterial;
		//wall.AddComponent<MeshCollider>();
		wall.AddComponent<BoxCollider>();
		// We can add this script when the ball gets the powerups that makes it able to destroy walls
		wall.AddComponent("WallCollisionScript");
		// Change so that the force of the ball decides how many meshes a wall should be destroyed in
		wall.AddComponent("SubdivideMeshScript");
		
		//wall.GetComponent<MeshCollider>().convex = true;
	 
		wall.layer = LayerMask.NameToLayer("Obstacles");
			
		wallList.Add(wall);
		return wall;
	}

	// This is almost the same script as CreateWallwithVertices, but here I add a rigidbody and not all scripts etc. is added.
	private GameObject CreateCrushedWall(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, Vector3 p5, Vector3 p6, Vector3 p7) {

		GameObject wallPart = new GameObject("Wall part");
		Mesh mesh = new Mesh();
		wallPart.AddComponent<MeshFilter>();
		wallPart.AddComponent<MeshRenderer>();

		Vector3[] vertices = new Vector3[]
		{
			// Bottom
			p0, p1, p2, p3,
		 
			// Left
			p7, p4, p0, p3,
		 
			// Front
			p4, p5, p1, p0,
		 
			// Back
			p6, p7, p3, p2,
		 
			// Right
			p5, p6, p2, p1,
		 
			// Top
			p7, p6, p5, p4
		};
		
		Vector3 up 		= Vector3.up;
		Vector3 down 	= Vector3.down;
		Vector3 front 	= Vector3.forward;
		Vector3 back 	= Vector3.back;
		Vector3 left 	= Vector3.left;
		Vector3 right 	= Vector3.right;
		 
		Vector3[] normales = new Vector3[]
		{
			// Bottom
			down, down, down, down,
		 
			// Left
			left, left, left, left,
		 
			// Front
			front, front, front, front,
		 
			// Back
			back, back, back, back,
		 
			// Right
			right, right, right, right,
		 
			// Top
			up, up, up, up
		};
		
		Vector2 _00 = new Vector2( 0f, 0f );
		Vector2 _10 = new Vector2( 1f, 0f );
		Vector2 _01 = new Vector2( 0f, 1f );
		Vector2 _11 = new Vector2( 1f, 1f );
		 
		Vector2[] uvs = new Vector2[]
		{
			// Bottom
			_11, _01, _00, _10,
		 
			// Left
			_11, _01, _00, _10,
		 
			// Front
			_11, _01, _00, _10,
		 
			// Back
			_11, _01, _00, _10,
		 
			// Right
			_11, _01, _00, _10,
		 
			// Top
			_11, _01, _00, _10,
		};
		
		int[] triangles = new int[]
		{
			// Bottom
			3, 1, 0,
			3, 2, 1,			
		 
			// Left
			3 + 4 * 1, 1 + 4 * 1, 0 + 4 * 1,
			3 + 4 * 1, 2 + 4 * 1, 1 + 4 * 1,
		 
			// Front
			3 + 4 * 2, 1 + 4 * 2, 0 + 4 * 2,
			3 + 4 * 2, 2 + 4 * 2, 1 + 4 * 2,
		 
			// Back
			3 + 4 * 3, 1 + 4 * 3, 0 + 4 * 3,
			3 + 4 * 3, 2 + 4 * 3, 1 + 4 * 3,
		 
			// Right
			3 + 4 * 4, 1 + 4 * 4, 0 + 4 * 4,
			3 + 4 * 4, 2 + 4 * 4, 1 + 4 * 4,
		 
			// Top
			3 + 4 * 5, 1 + 4 * 5, 0 + 4 * 5,
			3 + 4 * 5, 2 + 4 * 5, 1 + 4 * 5,
		 
		};
		 
		mesh.vertices = vertices;
		mesh.normals = normales;
		mesh.uv = uvs;
		mesh.triangles = triangles;
		 
		mesh.RecalculateBounds();
		mesh.Optimize();
		
		wallPart.AddComponent<Rigidbody>();
		wallPart.GetComponent<MeshFilter>().mesh = mesh;
		
		wallPart.renderer.material = wallMaterial;
		wallPart.AddComponent<MeshCollider>();
		wallPart.GetComponent<MeshCollider>().convex = true; //<-- if I want them to collide with each other
		
		// If time, the wall parts can be destroyed as well.
		//wallPart.AddComponent("SubdivideMeshScript");
		//wallPart.AddComponent("WallCollisionScript");
		
		return wallPart;
	}
	
	// When the ball collides with a whole wall, this method is callaed from WallCollisionScript
	// It iterates through the triangles on the front side of the wall and calls
	// CreateCrushedWall to split the wall in these
	public void CrushWallWrapper(GameObject obj){
		
		// ArrayList to store the crushed parts
		ArrayList arrayList = new ArrayList();

		Mesh mesh = obj.GetComponent<MeshFilter>().mesh;
		int[] tri = mesh.triangles;
		Vector3[] ver = mesh.vertices;
		ArrayList newTri = new ArrayList();
		
		// These two for-loops makes sure we only get the front triangles.
		// This is not general! It requires that that the wall has 8 triangles
		// on the front side. In WallCollisionScript, SubdivideMeshScript is
		// called first to make sure there are 8 triangles on the front side.
		for(int i = 12; i<18; i++){
			newTri.Add(tri[i]);	
		}
		for(int k = 36; k<tri.Length; k++){
			newTri.Add(tri[k]);	
		}
		tri = newTri.ToArray(typeof(int)) as int[];
		
		GameObject wallPart;
		
		// These for-loops takes the three vertices of each triangle on the
		// front side and makes a five new veritces of these, and these
		// eight vertices are then used to make a wall part
		// OBS THE SAME RIGHT NOW! seems to work, could be merged
		// start -> diagonal up -> left
		for(int b = 0; b<tri.Length; b+=6){
			
			Vector3 p0 = ver[tri[b]];
			Vector3 p4 = ver[tri[b+2]];
			Vector3 p5 = ver[tri[b+1]];
			Vector3 p1 = new Vector3(p5.x+0.01f, p5.y+0.01f, p5.z+0.01f);
			Vector3 p2 = new Vector3(p5.x+0.02f, p5.y+0.02f, p5.z-0.1f);
			Vector3 p3 = new Vector3(p0.x+0.01f, p0.y+0.01f, p0.z-0.1f);
			Vector3 p6 = new Vector3(p2.x+0.01f, p2.y+0.01f, p2.z+0.01f);
			Vector3 p7 = new Vector3(p4.x+0.01f, p4.y+0.01f, p4.z-0.1f);
			
			wallPart = CreateCrushedWall(p0, p1, p2, p3, p4, p5, p6, p7);
			wallPart.transform.position = obj.transform.position;
			wallPart.transform.rotation = obj.transform.rotation;
			arrayList.Add(wallPart);
		}
		// OBS THE SAME RIGHT NOW!
		// start -> right -> up
		for(int g = 3; g<tri.Length; g+=6){
			Vector3 p0 = ver[tri[g]];
			Vector3 p4 = ver[tri[g+2]];
			Vector3 p5 = ver[tri[g+1]];
			Vector3 p1 = new Vector3(p5.x+0.01f, p5.y+0.01f, p5.z+0.01f);
			Vector3 p2 = new Vector3(p5.x+0.02f, p5.y+0.02f, p5.z-0.1f);
			Vector3 p3 = new Vector3(p0.x+0.01f, p0.y+0.01f, p0.z-0.1f);
			Vector3 p6 = new Vector3(p2.x+0.01f, p2.y+0.01f, p2.z+0.01f);
			Vector3 p7 = new Vector3(p4.x+0.01f, p4.y+0.01f, p4.z-0.1f);
			
			wallPart = CreateCrushedWall(p0, p1, p2, p3, p4, p5, p6, p7);
			wallPart.transform.position = obj.transform.position;
			wallPart.transform.rotation = obj.transform.rotation;
			arrayList.Add(wallPart);
		}
		
		// Wait five secons and then remove the wall parts
		StartCoroutine(Wait(5.0f, arrayList));	
		
	}
	
	// Wait a little bit before removing destroyed wall parts
	private IEnumerator Wait(float seconds, ArrayList arrayList ) {
		
		float alhpa = 0.1f;
		
        yield return new WaitForSeconds(seconds);			
		
		foreach(GameObject g in arrayList){
			
			/*Color color = g.renderer.material.color;
			color.a -= 10000f;
			g.renderer.material.color = color;*/
			
			Destroy(g);
		}
	}
	
	// To call method X? and tell at what vertices the destroyed wall stood
	// This method could be done better, a bit to many declarations.. but it works :)
	public Vector3[] RemovedWallPos(GameObject removedWall){
		
		Vector3[] allVerts = removedWall.GetComponent<MeshFilter>().mesh.vertices;
		Vector3[] theVerts = new Vector3[2];
		
		// Adding vertices p0 and p2
		theVerts[0] = allVerts[0];
		theVerts[1] = allVerts[2];
		Vector3 p0 = removedWall.transform.position;
		Vector3 p2 = removedWall.transform.position;
		wallList.Remove(removedWall);
		
		int wallRot = (int) (removedWall.transform.rotation.eulerAngles.y);

		if(wallRot == 90){
			print ("yes");
			p0.x += theVerts[0].z;
			p0.z += theVerts[0].x;
			p2.x += theVerts[1].z;
			p2.z += theVerts[1].x;
		}
		else{
			p0.x += theVerts[0].x;
			p0.z += theVerts[0].z;
			p2.x += theVerts[1].x;
			p2.z += theVerts[1].z;
		}
		
		theVerts[0] = p0;
		theVerts[1] = p2;
		
		// Call Jeremys method here!! or something maybe in wallcollision. well see
		
		return theVerts;
	}
	
	/*void OnDrawGizmos(){
		Gizmos.DrawLine(points[0], points[1]);
	}*/
	
	
	
	/*********************OLD METHODS********************************/
	
	// Replaced by CrushWallWrapper
	// When the ball collides with the wall this methos is called. It iterated through
	// all triangles that mesh built up of and calls CreateCrushedWall with the vertices
	// that forms each triangle. Thus making of new object of each triangle so that
	// the wall looks like that it is crushed in different pieces. It also moves each wall
	// part so that it is at the right position in world space.
	public void CreateCrushedWallWrapper(GameObject obj) {
		ArrayList arrayList = new ArrayList();
		Mesh mesh = obj.GetComponent<MeshFilter>().mesh;
		int[] tri = mesh.triangles;
		Vector3[] ver = mesh.vertices;
		
		GameObject wallPart;
		
		for(int i = 0; i < tri.Length; i += 3) {			
			Vector3 p0 = ver[tri[i]];
			Vector3 p1 = ver[tri[i+1]];
			Vector3 p2 = ver[tri[i+2]];
			// The rest of the vertices are supposed to be using the same point in space
			// but I'm moving them a little bit since I'm making a box that looks like a triangle
			Vector3 p3 = new Vector3(p0.x + 0.01f, p0.y + 0.01f, p0.z + 1f);
			Vector3 p4 = new Vector3(p1.x + 0.01f, p1.y + 0.01f, p1.z + 1f);
			Vector3 p5 = new Vector3(p2.x + 0.01f, p2.y + 0.01f, p2.z + 0.9f);
			Vector3 p6 = new Vector3(p2.x + 0.01f, p2.y + 0.01f, p2.z + 1f);
			Vector3 p7 = new Vector3(p2.x + 0.01f, p2.y + 0.01f, p2.z + 1.1f);
					
			wallPart = CreateCrushedWall(p0, p1, p2, p3, p4, p5, p6, p7);
			
			// To get th wall part at the right position in worldspace
			// Considered Vector3 p0 = obj.transform.TransformPoint(ver[tri[i]]); also
			wallPart.transform.position = obj.transform.position;
			wallPart.transform.rotation = obj.transform.rotation;
			arrayList.Add(wallPart);
		}
		StartCoroutine(Wait(5.0f, arrayList));		
	}
	
	
	
	bool flag = true;
	
	//This method just cuts the wall in half
	public void CrashWall(GameObject wall, Vector3 collisionPoint){
		if(flag){
			Debug.Log ("Did it");
			
	        Mesh mesh = wall.GetComponent<MeshFilter>().mesh;
	        Vector3[] vertices = mesh.vertices;
	       /* int i = 0;
	        while (i < vertices.Length) {
	            vertices[i] += Vector3.up * Time.deltaTime;
	            i++;
	        }
	        mesh.vertices = vertices;
	        mesh.RecalculateBounds();*/
			
			// The new vertices
			Vector3 p8 = new Vector3(collisionPoint.x, vertices[0].y, vertices[0].z);
			Vector3 p9 = new Vector3(collisionPoint.x, vertices[3].y, vertices[3].z);
			Vector3 p10 = new Vector3(collisionPoint.x, vertices[4].y, vertices[4].z);
			Vector3 p11 = new Vector3(collisionPoint.x, vertices[5].y, vertices[5].z);
	
			
			Vector3 p0_L = vertices[0];
			Vector3 p1_L = p8;
			Vector3 p2_L = p9;
			Vector3 p3_L = vertices[3];	
			 
			Vector3 p4_L = vertices[5];
			Vector3 p5_L = p11;
			Vector3 p6_L = p10;
			Vector3 p7_L = vertices[4];
			
			Vector3 p0_H = p8;
			Vector3 p1_H = vertices[1];
			Vector3 p2_H = vertices[2];
			Vector3 p3_H = p9;	
			 
			Vector3 p4_H = p11;
			Vector3 p5_H = vertices[9];
			Vector3 p6_H = vertices[12];
			Vector3 p7_H = p10;
			
			Destroy(wall);
			
			GameObject tmp1 = CreateWallwithVertices(p0_L, p1_L, p2_L, p3_L, p4_L, p5_L, p6_L, p7_L);
			GameObject tmp2 = CreateWallwithVertices(p0_H, p1_H, p2_H, p3_H, p4_H, p5_H, p6_H, p7_H);
			
			tmp1.rigidbody.AddForce(Vector3.left*500);
			tmp2.rigidbody.AddForce(Vector3.right*500);
			flag = false;
			
		}
		else{
			//StartCoroutine(Wait(3.0f));
			flag = true;
		}
    } 

}
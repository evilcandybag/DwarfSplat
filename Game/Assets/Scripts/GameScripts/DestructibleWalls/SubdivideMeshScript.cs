using UnityEngine;
using System.Collections;

/// <summary>
/// This script is rewritten from javascript to C# by me Julia Adamsson 2013.
/// The original script is found here http://wiki.unity3d.com/index.php?title=MeshSubdivision
/// </summary>

public class SubdivideMeshScript : MonoBehaviour {

	//Material material;
	bool buildmesh = false;
	private Vector3[] verts;
	private Vector3[] norms;
	private Vector2[] uvs;
	private int[] trigs;
	private Mesh mesh;
	private Mesh originalMesh;
	//bool subdivision = false;
	//bool useobjectsmaterial = false;
	//bool forceaddmeshcollider  = false;
	//bool sides  = false;
	//bool middle = false;
	
	void Update () {
		/*if(Input.GetKeyDown("1"))			
			Subdivide(false);
		if(Input.GetKeyDown("2"))
			Subdivide(true);
		if(Input.GetKeyDown("x"))
			CopyMesh(originalMesh, mesh);*/
		
		
		
	}
	
	void Awake(){
		
		/**********Init*******/
		
		//buildmesh = false;
		if(GetComponent<MeshFilter>() == null){
			buildmesh = true;
			gameObject.AddComponent<MeshFilter>();
		}
		if(GetComponent<MeshRenderer>() == null)
			gameObject.AddComponent<MeshRenderer>();
		
		//if(forceaddmeshcollider) {
		//	if(GetComponent<MeshCollider>() == null)
		//		gameObject.AddComponent<MeshCollider>());
		//}

		Updatemesh();
		
		//if(!useobjectsmaterial)
		//	GetComponent<MeshRenderer>().material = material;
		
		/**********Build mesh*******/
		
		if(buildmesh){
			
			verts = new Vector3[] { new Vector3(0,-1f,0), new Vector3(1f,1f,0), new Vector3(-1f,1f,0), new Vector3(0,1f,-1f) };
			uvs = new Vector2[] { new Vector2(0,0), new Vector2(0,1f), new Vector2(1f,0), new Vector2(0,0) };
			trigs = new int[] { 0,1,2,1,3,2 };
			
			Applymesh();
			mesh.RecalculateNormals();

		}
		
		originalMesh = new Mesh();
		CopyMesh(mesh, originalMesh);
	}
	
	void Subdivide(bool center){

		verts = mesh.vertices;
		trigs = mesh.triangles;
		uvs = mesh.uv;
		norms = mesh.normals;
		
		Debug.Log("enter subdividing: " + verts.Length);
		
		ArrayList nv = new ArrayList();
		nv.AddRange(verts);
		ArrayList nt = new ArrayList();
		nt.AddRange(trigs);
		ArrayList nu = new ArrayList();
		nu.AddRange(uvs);
		ArrayList nn = new ArrayList();
		nn.AddRange(norms);
		
		if(!center){
			Debug.Log("Before: " + nt.Count);
			for(int i = 2; i < trigs.Length; i+=3){

				int p0trigwho = trigs[i-2];
				int p1trigwho = trigs[i-1];
				int p2trigwho = trigs[i];
	 
				int p0trigwhere = i-2;
				int p1trigwhere = i-1;
				int p2trigwhere = i;
	 
				Vector3 p0 = verts[p0trigwho];
				Vector3 p1 = verts[p1trigwho];
				Vector3 p2 = verts[p2trigwho];
	 
				Vector3 pn0 = norms[p0trigwho];
				Vector3 pn1 = norms[p1trigwho];
				Vector3 pn2  = norms[p2trigwho];
	 
				Vector2 pu0  = uvs[p0trigwho];
				Vector2 pu1  = uvs[p1trigwho];
				Vector2 pu2 = uvs[p2trigwho];
	 
				Vector3 p0mod  = (p0+p1)/2;	
				Vector3 p1mod  = (p1+p2)/2;
				Vector3 p2mod  = (p0+p2)/2;
	 
				Vector3 pn0mod = ((pn0+pn1)/2).normalized;	
				Vector3 pn1mod = ((pn1+pn2)/2).normalized;
				Vector3 pn2mod  = ((pn0+pn2)/2).normalized;
	 
				Vector2 pu0mod  = (pu0+pu1)/2;	
				Vector2 pu1mod  = (pu1+pu2)/2;
				Vector2 pu2mod = (pu0+pu2)/2;
	 
				int p0modtrigwho = nv.Count;
				int p1modtrigwho = nv.Count+1;
				int p2modtrigwho = nv.Count+2;
	 
				nv.Add(p0mod);
				nv.Add(p1mod);
				nv.Add(p2mod);
	 
				nn.Add(pn0mod);
				nn.Add(pn1mod);
				nn.Add(pn2mod);
	 
				nu.Add(pu0mod);
				nu.Add(pu1mod);
				nu.Add(pu2mod);
				
				nt[p0trigwhere] = p0trigwho;
				nt[p1trigwhere] = p0modtrigwho;
				nt[p2trigwhere] = p2modtrigwho;
	 
				nt.Add(p0modtrigwho);
				nt.Add(p1modtrigwho);
				nt.Add(p2modtrigwho);
	 
				nt.Add(p0modtrigwho);
				nt.Add(p1trigwho);
				nt.Add(p1modtrigwho);
	 
				nt.Add(p2modtrigwho);
				nt.Add(p1modtrigwho);
				nt.Add(p2trigwho);
				Debug.Log("in: " + nt.Count);
			}
			Debug.Log("After: " + nt.Count);
		}
		else{
			
			for(int ii = 2; ii < trigs.Length; ii+=3) {
 				

				int p0trigwhomi = trigs[ii-2];
				int p1trigwhomi = trigs[ii-1];
				int p2trigwhomi = trigs[ii];
	 
				int p0trigwheremi = ii-2;
				int p1trigwheremi = ii-1;
				int p2trigwheremi = ii;
	 
				Vector3 p0mi = verts[p0trigwhomi];
				Vector3 p1mi = verts[p1trigwhomi];
				Vector3 p2mi = verts[p2trigwhomi];
	 
				Vector3 p0mn = norms[p0trigwhomi];
				Vector3 p1mn = norms[p1trigwhomi];
				Vector3 p2mn = norms[p2trigwhomi];
	 
				Vector2 p0mu = uvs[p0trigwhomi];
				Vector2 p1mu = uvs[p1trigwhomi];
				Vector2 p2mu = uvs[p2trigwhomi];
	 
				Vector3 p0modmi = (p0mi+p1mi+p2mi)/3;
				Vector3 p0modmn = ((p0mn+p1mn+p2mn)/3).normalized;
				Vector2 p0modmu = (p0mu+p1mu+p2mu)/3;	
	 
				int p0modtrigwhomi = nv.Count;
	 
				nv.Add(p0modmi);
				nn.Add(p0modmn);
				nu.Add(p0modmu);
	 
				nt[p0trigwheremi] = p0trigwhomi;
				nt[p1trigwheremi] = p1trigwhomi;
				nt[p2trigwheremi] = p0modtrigwhomi;
	 
				nt.Add(p0modtrigwhomi);
				nt.Add(p1trigwhomi);
				nt.Add(p2trigwhomi);
	 
				nt.Add(p0trigwhomi);
				nt.Add(p0modtrigwhomi);
				nt.Add(p2trigwhomi);	
			}
		}
		
		verts = nv.ToArray(typeof(Vector3)) as Vector3[];
		norms = nn.ToArray(typeof(Vector3)) as Vector3[];
		uvs = nu.ToArray(typeof(Vector2)) as Vector2[];
		trigs = nt.ToArray(typeof(int)) as int[];
	 
		//Applyuvs();
		Applymesh();
		//mesh.RecalculateNormals();

		Debug.Log("exit subdividing: "+verts.Length);
	}
	
	void Applyuvs(){
		uvs = new Vector2[verts.Length];
		for(int i = 0; i < verts.Length; i++)
			uvs[i] = new Vector2(verts[i].x,verts[i].y);	
	}
	
	void Updatemesh() {
		//mesh = new Mesh();
		mesh = GetComponent<MeshFilter>().mesh;
	}
	 
	void Applymesh() {
		print(verts.Length);
		if(verts.Length > 65000){
			Debug.Log("Exiting... Too many vertices");
			return;
		}
		mesh.Clear();
		mesh.vertices = verts;
		mesh.uv = uvs;
		if(mesh.uv2 != null)
			mesh.uv2 = uvs;
		mesh.normals = norms;
		mesh.triangles = trigs;
		mesh.RecalculateBounds();
		if(GetComponent<MeshCollider>() != null)
			GetComponent<MeshCollider>().sharedMesh = mesh;
		Updatemesh();
	}
	 
	void CopyMesh(Mesh fromMesh, Mesh toMesh){
		toMesh.Clear();
		toMesh.vertices=fromMesh.vertices;
		toMesh.normals=fromMesh.normals;
		toMesh.uv=fromMesh.uv;
		toMesh.triangles=fromMesh.triangles;
	}
	
	public void MySubdivide(bool center){

		verts = mesh.vertices;
		trigs = mesh.triangles;
		uvs = mesh.uv;
		norms = mesh.normals;
		
		Debug.Log("enter subdividing: " + verts.Length);
		
		ArrayList nv = new ArrayList();
		nv.AddRange(verts);
		ArrayList nt = new ArrayList();
		nt.AddRange(trigs);
		ArrayList nu = new ArrayList();
		nu.AddRange(uvs);
		ArrayList nn = new ArrayList();
		nn.AddRange(norms);
		
		if(!center){
			Debug.Log("Before: " + nt.Count);
			for(int i = 14; i < 18; i+=3){
				// i < trigs.Length
				int p0trigwho = trigs[i-2];
				int p1trigwho = trigs[i-1];
				int p2trigwho = trigs[i];
	 
				int p0trigwhere = i-2;
				int p1trigwhere = i-1;
				int p2trigwhere = i;
	 
				Vector3 p0 = verts[p0trigwho];
				Vector3 p1 = verts[p1trigwho];
				Vector3 p2 = verts[p2trigwho];
	 
				Vector3 pn0 = norms[p0trigwho];
				Vector3 pn1 = norms[p1trigwho];
				Vector3 pn2  = norms[p2trigwho];
	 
				Vector2 pu0  = uvs[p0trigwho];
				Vector2 pu1  = uvs[p1trigwho];
				Vector2 pu2 = uvs[p2trigwho];
	 			
				Vector3 p0mod  = (p0+p1)/2;	
				Vector3 p1mod  = (p1+p2)/2;
				Vector3 p2mod  = (p0+p2)/2;
				
	 
				Vector3 pn0mod = ((pn0+pn1)/2).normalized;	
				Vector3 pn1mod = ((pn1+pn2)/2).normalized;
				Vector3 pn2mod  = ((pn0+pn2)/2).normalized;
	 
				Vector2 pu0mod  = (pu0+pu1)/2;	
				Vector2 pu1mod  = (pu1+pu2)/2;
				Vector2 pu2mod = (pu0+pu2)/2;
	 
				int p0modtrigwho = nv.Count;
				int p1modtrigwho = nv.Count+1;
				int p2modtrigwho = nv.Count+2;
	 
				nv.Add(p0mod);
				nv.Add(p1mod);
				nv.Add(p2mod);
	 
				nn.Add(pn0mod);
				nn.Add(pn1mod);
				nn.Add(pn2mod);
	 
				nu.Add(pu0mod);
				nu.Add(pu1mod);
				nu.Add(pu2mod);
				
				nt[p0trigwhere] = p0trigwho;
				nt[p1trigwhere] = p0modtrigwho;
				nt[p2trigwhere] = p2modtrigwho;
	 
				nt.Add(p0modtrigwho);
				nt.Add(p1modtrigwho);
				nt.Add(p2modtrigwho);
	 
				nt.Add(p0modtrigwho);
				nt.Add(p1trigwho);
				nt.Add(p1modtrigwho);
	 
				nt.Add(p2modtrigwho);
				nt.Add(p1modtrigwho);
				nt.Add(p2trigwho);
				Debug.Log("in: " + nt.Count);
			}
			Debug.Log("After: " + nt.Count);
		}
		
		verts = nv.ToArray(typeof(Vector3)) as Vector3[];
		norms = nn.ToArray(typeof(Vector3)) as Vector3[];
		uvs = nu.ToArray(typeof(Vector2)) as Vector2[];
		trigs = nt.ToArray(typeof(int)) as int[];
	 
		//Applyuvs();
		Applymesh();
		//mesh.RecalculateNormals();

		Debug.Log("exit subdividing: "+verts.Length);
	}
}

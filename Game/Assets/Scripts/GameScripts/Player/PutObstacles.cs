using UnityEngine;
using System.Collections;

public class PutObstacles : MonoBehaviour {
	
	public Transform wall;
	
	public Transform floor;
	private TileGraphGenerator gen;
	
	// Use this for initialization
	void Start () {
		gen = floor.GetComponent<TileGraphGenerator>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetMouseButtonDown(0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 100.0f)) {
				//Vector3 hitPos = Camera.main.WorldToScreenPoint(hit.point);
				hit.point.Set(hit.point.x, 1, hit.point.z);
				Vector3 newWall = new Vector3(hit.point.x, 1, hit.point.z);
				Instantiate(wall, newWall, Quaternion.identity);
			
				// TODO: better way to find the width of an object? 
				Vector3 size = wall.GetComponent<Renderer>().bounds.size;
				// Rescan(Vector3 bottomLeft, Vector3 topRight)
				gen.Rescan(newWall , newWall + size);
			}
		}
		
	}
}

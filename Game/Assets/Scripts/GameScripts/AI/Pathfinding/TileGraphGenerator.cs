using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Pathfinding.Core;
using Pathfinding.Graph;
	
public class TileGraphGenerator : MonoBehaviour {

	public int width = 100;
	public int depth = 100;
	public float height = 1000f;
	
	public bool drawDebugGraph = false;
	
	public TileGraph tileGraph;
	
	LayerMask layerFloor, layerObstacles;
	LayerMask layer;

	void Awake() {
		// setup the layers to detect the floor and the obstacles
		layerFloor = LayerMask.NameToLayer("Floor");
		layerObstacles = LayerMask.NameToLayer("Obstacles");
		
		layer = (1 << layerFloor) | (1 << layerObstacles);
		
		Scan(); // scan the map
		RadiusModifier(1); // extend the unwalkable areas

	}
	
	void Update () {
		if (drawDebugGraph) {
			tileGraph.drawDebugGraph();	
		}
	}
	
	/** Scan the map with a raycast */
	public void Scan() {
		
		TileNode[,] nodes = new TileNode[width, depth];
		
		// maybe not the proper way (use the MeshFilter component ?)
		Vector3 center = GetComponent<Renderer>().bounds.center;
		Vector3 size = GetComponent<Renderer>().bounds.size;
		
		// consider the mesh as a rectangle
		Vector3 startPos = center - size / 2;
		float cellWidth = size.x / width;
		float cellDepth = size.z / depth;
		
		// raycast position
		Vector3 pos = new Vector3(0,0,0);
		pos.y = height;
		
		for (int x = 0; x < width; x++) {
			for (int z = 0; z < depth; z++) {
				pos = startPos + new Vector3(x * cellWidth, height, z * cellDepth);
				RaycastHit hit;
				if (Physics.Raycast(pos, -Vector3.up, out hit, Mathf.Infinity, layer)) {
					nodes[x,z] = new TileNode(hit.point, x*width+z, x, z);
					
					if (hit.transform.gameObject.layer == layerObstacles) {
						
						nodes[x,z].Walkable = false;
					}
				}
				
			}
		}
		
		tileGraph = new TileGraph(nodes, width, depth, cellWidth, cellDepth, startPos);
		
	}
	
	/** 
	 * Extend the unwalkable areas with the given radius
	 */
	public void RadiusModifier(int radius = 0) {
		
		List<Node> newObstacles = new List<Node>();
		
		for (int x = 0; x < width; x++) {
			for (int z = 0; z < depth; z++) {
				List<Node> neighbors = tileGraph.GetNeighbors(x, z, radius);
				if (neighbors != null) {
					newObstacles.AddRange(neighbors);
				}
			}
		}
		
		for (int i = 0; i < newObstacles.Count; ++i) {
			newObstacles[i].Walkable = false;	
		}
		
	}
	
}
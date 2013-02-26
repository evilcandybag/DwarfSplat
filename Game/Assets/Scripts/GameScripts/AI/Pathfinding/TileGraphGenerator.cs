using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Pathfinding.Core;
using Pathfinding.Graph;
	
public class TileGraphGenerator : MonoBehaviour {

	public int width = 50;
	public int depth = 50;
	public float height = 1000f;
	
	// we assume that the map geometry does not change 
	protected Vector3 startPos;
	protected float cellWidth;
	protected float cellDepth;
	
	public int radius = 1;
	public bool drawDebugGraph = false;
	
	public TileGraph tileGraph;
	
	LayerMask layer, layerFloor, layerObstacles;

	void Start() {
		// setup the layers to detect the floor and the obstacles
		layerFloor = LayerMask.NameToLayer("Floor");
		layerObstacles = LayerMask.NameToLayer("Obstacles");
		
		layer = (1 << layerFloor) | (1 << layerObstacles);
		
		Scan(); // scan the map
		RadiusModifier(radius); // extend the unwalkable areas

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
		startPos = center - size / 2;
		cellWidth = size.x / width;
		cellDepth = size.z / depth;
		
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
	
	/** Rescan the ENTIRE map to detect new obstacles */
	public void Rescan() {
		Rescan(0, 0, width, depth);
	}
	
	/** Rescan a specific part of the map to detect new or gone obstacles */
	public void Rescan(Vector3 bottomLeft, Vector3 topRight) {
		
		TileNode bl = tileGraph.GetNode(bottomLeft);
		TileNode tr = tileGraph.GetNode(topRight);
		
		if (bl == null || tr == null) return;
		
		Rescan(bl.X, bl.Z, tr.X, tr.Z);
		
	}
	
	/** Rescan a specific part of the map to detect new or gone obstacles 
	 *  Uses tiles coordinates
	 * */
	public void Rescan(int startX, int startZ, int endX, int endZ) {
	
		Vector3 pos = new Vector3(0,0,0); // raycast position
		pos.y = height;
		
		for (int x = startX; x < endX; x++) {
			for (int z = startZ; z < endZ; z++) {
				pos = startPos + new Vector3(x * cellWidth, height, z * cellDepth);
				RaycastHit hit;
				if (Physics.Raycast(pos, -Vector3.up, out hit, Mathf.Infinity, layer)) {
					tileGraph.SetWalkable(x,z,true);
					if (hit.transform.gameObject.layer == layerObstacles) {
						tileGraph.SetWalkable(x,z,false);
					}
				}
			}
		}
		
		RadiusModifier(startX, startZ, endX, endZ, radius);
		
	}
	
	/** 
	 * Extend the unwalkable areas with the given radius
	 * for the ENTIRE MAP
	 */
	public void RadiusModifier(int r = 0) {
		RadiusModifier(0, 0, width, depth, r);
	}
	
	/** 
	 * Extend the unwalkable areas with the given radius
	 * for the given position and dimensions
	 */
	public void RadiusModifier(int startX, int startZ, int endX, int endZ, int r = 0) {
		
		List<Node> newObstacles = new List<Node>();
		
		for (int x = startX; x < endX; x++) {
			for (int z = startZ; z < endZ; z++) {
				List<Node> neighbors = tileGraph.GetNeighbors(x, z, r);
				if (neighbors != null) {
					newObstacles.AddRange(neighbors);
				}
			}
		}
		
		for (int i = 0; i < newObstacles.Count; ++i) {
			newObstacles[i].Walkable = false;	
		}
		
	}
	
	/** Check if an area is walkabe or not */
	public bool IsWalkable(Vector3 bottomLeft, Vector3 topRight) {
		TileNode bl = tileGraph.GetNode(bottomLeft);
		TileNode tr = tileGraph.GetNode(topRight);
		
		if (bl == null || tr == null) return false;
		
		for (int x = bl.X; x < tr.X; x++)
			for (int z = bl.Z; z < tr.Z; z++)
				if (!tileGraph.IsWalkable(x,z))
					return false;
		
		return true;
	}
}
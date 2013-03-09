using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Pathfinding.Core;

namespace Pathfinding.Graph {

	public class TileGraph: Graph {
		
		protected int width;
		protected int depth;
		protected float cellWidth;
		protected float cellDepth;
		protected Vector3 startPos;
		
		protected Node[,] nodes;
		
		public TileGraph(Node[,] nodes, int width, int depth, float cellWidth, float cellDepth, Vector3 startPos) {
			this.nodes = nodes;
			this.width = width;
			this.depth = depth;
			this.cellWidth = cellWidth;
			this.cellDepth = cellDepth;
			this.startPos = startPos;
		}
		
		/** Find the closest node to a position which is walkable 
		 * The loop variable means that we will keep searching, if we don't 
		 * find any walkable node the first time.
		 * */
		protected override Node getClosestNode(Vector3 position, bool loop = true) {
			
			position -= startPos;
			// only x and z
			int i = Mathf.FloorToInt(position.x / cellWidth);
			int j = Mathf.FloorToInt(position.z / cellDepth);
			
			if (!checkBounds(i,j)) return null;
			
			// spiral looping to find the closest walkable node
			int x = 0, y = 0;
			if (loop) {
				int dx = 0, dy = -1;
				while (!nodes[i+x,j+y].Walkable) {
					if (x == y || (x < 0 && x == -y) || (x > 0 && x == 1 - y)) {
						int temp = dx;
						dx = -dy;
						dy = temp;
					}
					x += dx;
					y += dy;
					 
					// There may be a little problem with this test when the player is
					// close to the limit of the map
					if (!checkBounds(i+x,j+y)) return null;
				}
			}
			
			return nodes[i+x,j+y];
			
		}
		
		/** Return the node corresponding to the given position */
		public TileNode GetNode(Vector3 position) {
			position -= startPos;
			// only x and z
			int i = Mathf.FloorToInt(position.x / (float)cellWidth);
			int j = Mathf.FloorToInt(position.z / (float)cellDepth);
			
			if (!checkBounds(i,j)) return null;
			
			return (TileNode)nodes[i,j];
		}
		
		protected bool checkBounds(int x, int y) {
			if (x < 0 || x >= width || y < 0 || y >= depth) return false;
			return true;
		}
					
		
		public override void Reset() {
			for (int i = 0; i < width; ++i) {
				for (int j = 0; j < depth; ++j) {
					nodes[i,j].Reset();		
				}
			}
		}
		
		public void SetObstacle(int x, int z, bool obstacle) {
			SetWalkable(x,z,!obstacle);
			nodes[x,z].Obstacle = obstacle;
		}
		
		public void SetWalkable(int x, int z, bool walkable) {
			nodes[x,z].Walkable = walkable;
		}
		
		public bool IsWalkable(int x, int z) {
			return nodes[x,z].Walkable;	
		}
		
		protected override List<Node> getNeighbors(Node node, int radius = 1) {
			List<Node> neighbors = new List<Node>();
			TileNode currentNode = (TileNode)node;
			
			for (int i = currentNode.X - radius; i <= currentNode.X + radius; ++i) {
				for (int j = currentNode.Z - radius; j <= currentNode.Z + radius; ++j) {
					if (i >= 0 && i < width && j >= 0 && j < depth && nodes[i,j].Walkable && 
						nodes[i,j] != node) {
						neighbors.Add(nodes[i,j]);	
					}
				}
			}
			
			return neighbors;
		}
		
		/** 
		 * Get neighbors around the given position, with a given radius, 
		 * supposed to be used to extend unwalkable areas
		 */
		public List<Node> GetNeighbors(int x, int z, int radius = 1) {
			if (nodes[x,z] != null && nodes[x,z].Obstacle) {
				return getNeighbors(nodes[x,z], radius);
			} else {
				return null;	
			}
		}
		
		/** This heuristic returns the manhattan distance between 2 nodes */
		protected override float heuristic(Node a, Node b) {
			return diagonalShortcut(a, b);
		}
		
		/** Compute the manhattan heuristic */
		protected float manhattan(Node a, Node b) {
			float distance = 
				Mathf.Abs(b.Position.x - a.Position.x) +  
				Mathf.Abs(b.Position.z - a.Position.z)
			;
			return distance;
		}
		
		/** 
		 * Compute the diagonal shortcut heuristic, another kind of 
		 * heuristic  
		*/
		protected float diagonalShortcut(Node a, Node b) {
			float xDist = Mathf.Abs(a.Position.x - b.Position.x);
			float zDist = Mathf.Abs(a.Position.z - b.Position.z);
			if(xDist > zDist){
			   return 1.4f*zDist + (xDist-zDist);
			} else {
			   return 1.4f*xDist + (zDist-xDist);
			}
			
		}
		
		/** Compute the euclidian distance between two nodes */
		protected override float euclidian(Node a, Node b) {
			Vector2 first = new Vector2(a.Position.x, a.Position.z);
			Vector2 second = new Vector2(b.Position.x, b.Position.z);
			return Vector2.Distance(first, second);
			
		}
		
		
		/** Draw the tile graph */
		public override void drawDebugGraph() {
			
			for (int i = 0; i < width; ++i) {
				for (int j = 0; j < depth; ++j) {
					List<Node> neighbors = getNeighbors(nodes[i,j]);
					for (int n = 0; n < neighbors.Count; ++n) {
						Color color = Color.red;
						Debug.DrawLine(nodes[i,j].Position, neighbors[n].Position, color);	
					}
				}
			}
		}
			
	}
	
}

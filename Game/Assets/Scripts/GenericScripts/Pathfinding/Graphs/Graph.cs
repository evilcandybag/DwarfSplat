using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using Pathfinding.Common;
using Pathfinding.Core;

namespace Pathfinding.Graph {
	
	/** Callback when a new path is computed */
	public delegate void OnPathComputed(Path newPath);
	
	abstract public class Graph {
		
		protected PriorityQueue<Node> pq;
		protected Dictionary<Node, Node> parentPath;
		protected HashSet<Node> openSetHash;
		protected HashSet<Node> closedSetHash;
		
		public Graph() {
			pq = new PriorityQueue<Node>();
			parentPath = new Dictionary<Node, Node>();
			openSetHash = new HashSet<Node>();
			closedSetHash = new HashSet<Node>();
		}
		
		public void AStar(Vector3 start, Vector3 end, OnPathComputed callback) {
			
			// Reset all
			// This is O(n) complexity so it might be possible to
			// improve this with some sort of polling
			parentPath.Clear();
			pq.Clear();
			openSetHash.Clear();
			closedSetHash.Clear();
			Path path = new Path();
			
			Node startNode = getClosestNode(start);
			Node endNode = getClosestNode(end);
			
			if (startNode == null || endNode == null || start == end) {
				callback(path);
				return;
			}			
			
			pq.Push(startNode);
			openSetHash.Add(startNode);
			
			startNode.GScore = 0;
			startNode.FScore = startNode.GScore + heuristic(startNode, endNode);
			
			Node current = startNode;
			
			while (pq.Count() > 0) {
				current = pq.Pop(); // automatically do the remove part
				openSetHash.Remove(current);
				
				if (current.Id == endNode.Id) {
					path.Add(current.Position);
					while (parentPath.ContainsKey(current)) {
						current = parentPath[current];
						path.Add(current.Position);
					}
					path.Reverse();
					callback(path);
					return;
				}
				
				closedSetHash.Add(current);
				
				List<Node> neighbors = getNeighbors(current);
				
				for (int i = 0; i < neighbors.Count; ++i) {
					Node neighbor = neighbors[i];
					if (closedSetHash.Contains(neighbor)) continue;
					
					float gAttempt = current.GScore + euclidian(current, neighbor);
					
					if (!openSetHash.Contains(neighbor) || gAttempt <= neighbor.GScore) {
						parentPath[neighbor] = current;
						neighbor.GScore = gAttempt;
						neighbor.FScore = neighbor.GScore + heuristic(neighbor, endNode);
						if (!openSetHash.Contains(neighbor)) {
							pq.Push(neighbor);
							openSetHash.Add(neighbor);
						}
					}
				} // end loop neighbors
			}
		}
		
		/** Re-initialize all the nodes of the graph */
		abstract public void Reset();
		
		/** Return the closest Node of the graph based on the given Position */
		abstract protected Node getClosestNode(Vector3 Position, bool loop = true);
		
		/** Return all the neighbors of the given Node */
		abstract protected List<Node> getNeighbors(Node node, int radius = 1);
		
		/** Delegates the computation to a specific graph */
		abstract protected float heuristic(Node a, Node b);
		
		/** Delegate the computation to a specific graph */
		abstract protected float euclidian(Node a, Node b);
		
		/** Draw a debug graph in the Unity editor */ 
		abstract public void drawDebugGraph();
		
	}
	
}



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
		
		public void AStar(Vector3 start, Vector3 end, OnPathComputed callback) {
			
			// Reset all scores and sets membership
			Reset();
			
			Node startNode = getClosestNode(start);
			Node endNode = getClosestNode(end);
			
			if (startNode == null || endNode == null || start == end) {
				callback(new Path());
				return;
			}
			
			PriorityQueue<Node> openSet = new PriorityQueue<Node>();
			
			openSet.Push(startNode); startNode.InOpenSet = true;
			
			Dictionary<Node, Node> parentPath = new Dictionary<Node, Node>();
			
			startNode.GScore = 0;
			startNode.FScore = startNode.GScore + heuristic(startNode, endNode);
			
			Path path = new Path();
			Node current = startNode;
			
			while (openSet.Count() > 0) {
				current = openSet.Pop(); // automatically do the remove part
				current.InOpenSet = false;

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
				
				current.InClosedSet = true;
				
				List<Node> neighbors = getNeighbors(current);
				
				for (int i = 0; i < neighbors.Count; ++i) {
					Node neighbor = neighbors[i];
					if (neighbor.InClosedSet) continue;
					
					float gAttempt = current.GScore + euclidian(current, neighbor);
					
					if (!neighbor.InOpenSet || gAttempt <= neighbor.GScore) {
						parentPath[neighbor] = current;
						neighbor.GScore = gAttempt;
						neighbor.FScore = neighbor.GScore + heuristic(neighbor, endNode);
						if (!neighbor.InOpenSet) {
							openSet.Push(neighbor);
							neighbor.InOpenSet = true;
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



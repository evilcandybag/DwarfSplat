using UnityEngine;
using System;
using System.Collections;

/**
 * Class to represent a general node
 * It is used to store the f and g score requires by the 
 * A* algorithm
 */

namespace Pathfinding.Core {
	
	public class Node : IComparable<Node> {
				
		protected int id;
		protected Vector3 position;
		protected bool walkable = true;
		protected bool obstacle = false;
		protected float gScore = 0;
		protected float fScore = float.MaxValue;
		
		public Node() {}
		
		public Node(Vector3 position, int id) {
			this.id = id;
			this.position = position;
		}
		
		public void Reset() {
			this.gScore = 0;
			this.fScore = float.MaxValue;
		}
		
		public int CompareTo(Node node) {
			if (this.fScore < node.fScore) return -1;
			else if (this.fScore > node.fScore) return 1;
			else return 0;
		}
		
		public override string ToString () {
			// just print the f score to debug the priority queue
			return fScore.ToString();
		}

		public int Id {
			get {
				return this.id;
			}
		}		
		
		public Vector3 Position {
			get {
				return this.position;
			}
			set {
				position = value;
			}
		}

		public bool Walkable {
			get {
				return this.walkable;
			}
			set {
				walkable = value;
			}
		}

		public bool Obstacle {
			get {
				return this.obstacle;
			}
			set {
				obstacle = value;
			}
		}
		public float GScore {
			get {
				return this.gScore;
			}
			set {
				gScore = value;
			}
		}

		public float FScore {
			get {
				return this.fScore;
			}
			set {
				fScore = value;
			}
		}
	}
}
using UnityEngine;
using System.Collections;

namespace Pathfinding.Core {
	
	public class TileNode : Node {

		protected int x;
		protected int z;
		
		public TileNode(Vector3 pos, int id, int x, int z): base(pos, id){
			this.x = x;
			this.z = z;
		}
		
		public int X {
			get {
				return this.x;
			}
		}

		public int Z {
			get {
				return this.z;
			}
		}
	}
	
}


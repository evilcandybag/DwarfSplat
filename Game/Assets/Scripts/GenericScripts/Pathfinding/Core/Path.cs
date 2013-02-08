using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Pathfinding.Core {
	
	/** 
	 * Structure for storing a path.
	 */
	
	public class Path : List<Vector3> {
	
		public void drawDebugPath(Color color) {
			if (Count > 0) {
				for (int i = 0; i < Count-1; ++i) {
					Debug.DrawLine(this[i], this[i+1], color);
				}
			} else {
				Debug.Log("No path found!!!");	
			}
			
		}
		
	}

}
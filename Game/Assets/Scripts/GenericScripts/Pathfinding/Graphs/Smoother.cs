using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Pathfinding.Core;

namespace Pathfinding.Graph {
	
	/**
	 * Smooth a given path to make it look more realistic
	 */
	public class Smoother {
		
		public static Path smoothPath(Path inputPath) {
			
			if (inputPath.Count <= 2) return inputPath;
			
			Path outputPath = new Path();
			outputPath.Add(inputPath[0]);
			
			for (int i = 2; i < inputPath.Count - 1; ++i) {				
				
				float distance = Vector3.Distance(outputPath[outputPath.Count - 1], inputPath[i]);
				// be careful of the direction of the ray
				Vector3 direction = (inputPath[i] - outputPath[outputPath.Count - 1]).normalized;
				if (Physics.Raycast(outputPath[outputPath.Count - 1], direction, distance, 1 << 8)) {
					outputPath.Add(inputPath[i - 1]);
				}
			}
			
			outputPath.Add(inputPath[inputPath.Count - 1]);
			
			return outputPath;
			
		}
		
	}
	
	
}
using UnityEngine;
using System.Collections;

using Pathfinding.Core;
using Pathfinding.Graph;

public class Follow : MonoBehaviour {
	
	public Transform target;
	public Transform floor;
	public float speed = 100;
	public float wayPointDistance = 0.3f;
	
	protected CharacterController controller;
	protected Graph graph = null;
	
	private Vector3 targetPosition;
	private Path path;
	private int currentPos = 0;
	
	/* 
	 * Refresh interval for the path
	 * It prevents the path to be recomputed too often, and during 
	 * this time the AI does not get stuck, it keep moving
	 */
	public float TIME_REPATH = 1;
	private float lastPath = 0;
	private Vector3 currentPoint;
	
	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		
		targetPosition = new Vector3(target.position.x, target.position.y, target.position.z);
		currentPoint = new Vector3(0,0,0);
	}
	
	/* When a new path has been computed */
	void onCallback(Path newPath) {
		path = newPath;
		currentPos = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
		graph = floor.GetComponent<TileGraphGenerator>().tileGraph;
		if (graph == null) {
			Debug.Log ("no graph");
			return;
		}
		
		lastPath += Time.deltaTime;
		
		// recompute a path only after a delay of 1ms, or when the target moved (also after the delay)
		if ((target.position != targetPosition && lastPath > TIME_REPATH) || lastPath > TIME_REPATH) {
			lastPath = 0;
			targetPosition.Set (target.position.x, target.position.y, target.position.z);
			graph.AStar(transform.position, targetPosition, new OnPathComputed(onCallback));
		}
		
		if (path == null || currentPos >= path.Count) return;
		
		currentPoint.Set(path[currentPos].x, transform.position.y, path[currentPos].z);
		
		Vector3 direction = (currentPoint - transform.position).normalized;
		direction *= speed * Time.deltaTime;
		controller.SimpleMove(direction);
		
		if (Vector3.Distance(transform.position, currentPoint) < wayPointDistance) {
			currentPos++;
		}
		
		path.drawDebugPath(Color.green);
		
	}
	
}
